from pprint import pprint
import time
import re
import os
import platform
import sys
import json
from requests import Session
import requests
import xml.etree.ElementTree as ET
from robobrowser import RoboBrowser
from bs4 import BeautifulSoup
from clint.textui import progress

# Author Martin Bobčík
# exec(open("filename.py").read())

# surpress warnings, mainly due to no certificates
import warnings
warnings.filterwarnings("ignore")

#regex = r"(?<=<\/h3>\s).*(?=<br\/?><br\/?>\s)"
regex = r"(?<=<\/h3>\s).*\s.*(?=<br\/?><br\/?>\s)"
__location__ = os.path.realpath(os.path.join(os.getcwd(), os.path.dirname(__file__)))
config = json.load(open(os.path.join(__location__,'fitLoader.config.json')))
systemAwareFolderDelimiter = '\\' if platform.system() == 'Windows' else '/'
soafd = systemAwareFolderDelimiter

# create new session disabling certificate checking
session = Session()
session.verify = False
browser = RoboBrowser(session=session)

def sanitizeFileName(path):
    path = path.replace('ø', 'ř')
    path = path.replace('¹', 'š')
    path = path.replace("\r\n", " ")
    path = path.replace(' ', '_')
    path = path.replace('.', '')
    path = path.replace(',', '')
    path = path.replace('¾', 'ž')
    path = path.replace('è', 'č')
    path = path.replace('ì', 'ě')
    path = path.replace('»','ť')
    path = path.replace('ù', 'ů')
    path = path.replace('Ø','Ř')
    return path

def downloadWithProgress(url, path):
    r = browser.session.get(url, stream=True, verify=False)
    with open(path, 'wb') as f:
        total_length = int(r.headers.get('content-length'))
        for chunk in progress.bar(r.iter_content(chunk_size=1024), expected_size=(total_length / 1024) + 1):
            if chunk:
                f.write(chunk)
                f.flush()

#procompiledRegex = re.compile(regex, re.MULTILINE | re.DOTALL)

# log in to FIT
browser.open('https://cas.fit.vutbr.cz')

form = browser.get_form(action='/cosign.cgi')
form['login'].value = config["login"]
form['password'].value = config["password"]
browser.submit_form(form)

# open video server
browser.open('https://video1.fit.vutbr.cz/av/records-categ.php?id=1')

# get links to semesters
links = browser.get_links(class_='link')
del links[:1]
# regex - get link names
# ^<a class="link" .+id=\d{1,4}">(.+)<\/a>
# but use of BeautifullSoup is advised instead

# BeatifulSoup(html_doc)
# titleName = soup.title.name

print("\nSemesters")
i = 0
for link in links:
    print("{} - {}".format(i, link.text))
    i += 1

# select semester
choice = input("Select semester: ")
browser.follow_link(links[int(choice)])

links = browser.get_links(class_='link')
del links[:2]
print("\nSubjects")
i = 0
for link in links:
    print("{} - {}".format(i, link.text))
    i += 1

# select subject
choice = input("Select subject: ")
SubjectLink = links[int(choice)]
browser.follow_link(SubjectLink)

SubjectName = SubjectLink.text[:3]
print(SubjectName)

# KOMENTOVAT JE POTREBA!!!!!
links = browser.get_links(class_='link')
MSTeamsZaznamy = None
if "Záznamy MS Teams" in links[3].text:
    MSTeamsZaznamy = links[3]
    del links[:4]
else:
    del links[:3]

# pprint(links)
# print("\n")    

i = 0
fileLinksList = []
for link in links:
    # print("{} - {}".format(i, link.text))
    i += 1
    browser.follow_link(link)
    matches = re.search(regex, str(browser.parsed), re.MULTILINE )
    #print(link)
    #print(matches)
    fileLinksList.append((sanitizeFileName(matches.group()), browser.get_links(class_="button")[1].attrs['href']))
    # browser.follow_link(SubjectLink)# get back to page with lectures

print("Lectures found: {}".format(len(fileLinksList)))
i = 0
for link in fileLinksList:
    i += 1
    print('\t{} {}'.format(i, link[0]))

#skipFirstNLectures = int(input("Skip first n lectures ( 0 to download all ): "))
#skipLastNLectures = int(input("Skip last n lectures ( 0 to download all ): "))
#
#if skipFirstNLectures >= len(fileLinksList) - skipLastNLectures:
#    print("No Lectures to download. skipFirstNLectures >= skipLastNLectures")
#    exit()
#
#del fileLinksList[:skipFirstNLectures]
#del fileLinksList[-skipLastNLectures:]
#
#print("Lectures designed to be downloaded:")
#i = skipFirstNLectures
i=0
#for link in fileLinksList:
#    i += 1
#    print('\t{} {}'.format(i, link[0]))

if not os.path.isdir("{}{}{}".format(config["path"], soafd, SubjectName)):
    os.makedirs("{}{}{}".format(config["path"], soafd, SubjectName))

#i = skipFirstNLectures
i=0
for link in fileLinksList:
    i += 1
    print('{} {}{}{}{}{}.mp4'.format(i, config["path"], soafd, SubjectName, soafd, link[0]))
    # stáhnout
    downloadWithProgress(link[1], '{}{}{}{}{}_{}.mp4'.format(config["path"], soafd, SubjectName, soafd, i, link[0]))
