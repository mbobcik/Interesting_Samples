from pprint import pprint
import time
import re
import os
import sys
import json
from requests import Session
import requests
import xml.etree.ElementTree as ET
from robobrowser import RoboBrowser
from bs4 import BeautifulSoup
from clint.textui import progress

##Author Martin Bobčík
#exec(open("filename.py").read())

#surpress warnings, mainly due to no certificates
import warnings
warnings.filterwarnings("ignore")

regex = r"(?<=<\/h3>\s).*(?=<br\/?><br\/?>\s?<a)"
config = json.load(open('fitLoader.config.json'))

#create new session disabling certificate checking
session = Session()
session.verify = False
browser = RoboBrowser(session = session)

def sanitizeFileName(path):
	return path.replace('ø','ř').replace('¹','š').replace("\r\n"," ").replace(' ','_').replace('.','').replace(',' , '') 
	pass

def downloadWithProgress(url, path):
	r =  browser.session.get(url, stream=True, verify=False)
	with open(path, 'wb') as f:
	    total_length = int(r.headers.get('content-length'))
	    for chunk in progress.bar(r.iter_content(chunk_size = 1024), expected_size = (total_length / 1024) + 1): 
	        if chunk:
	            f.write(chunk)
	            f.flush()



#procompiledRegex = re.compile(regex, re.MULTILINE | re.DOTALL)

#log in to FIT 
browser.open('https://cas.fit.vutbr.cz')

form = browser.get_form(id='rightUIcol')
form['login'].value = config["login"]
form['password'].value =  config["password"]
browser.submit_form(form)

#open video server
browser.open('https://video1.fit.vutbr.cz/av/records-categ.php?id=1')

#get links to semesters
links = browser.get_links(class_='link');
del links[:1]
#regex - get link names
# ^<a class="link" .+id=\d{1,4}">(.+)<\/a>
# but use of BeautifullSoup is advised instead

#BeatifulSoup(html_doc)
#titleName = soup.title.name

print("\nSemesters")
i = 0
for link in links:
    print("{} - {}".format(i, link.text))
    i += 1

#select semester
choice = input("Select semester: ")
browser.follow_link(links[int(choice)])

links = browser.get_links(class_ = 'link');
del links[:2]
print("\nSubjects")
i = 0
for link in links:
	print("{} - {}".format(i, link.text))
	i += 1

#select subject
choice = input("Select subject: ")
SubjectLink = links[int(choice)]
browser.follow_link(SubjectLink)

SubjectName = SubjectLink.text[:3]
print(SubjectName)

###########KOMENTOVAT JE POTREBA!!!!!

links = browser.get_links(class_ = 'link');
del links[:3]
#pprint(links)
#print("\n")
i = 0
fileLinksList = []
for link in links:
	#print("{} - {}".format(i, link.text))
	i += 1
	browser.follow_link(link)
	

	matches = re.search(regex, str(browser.parsed), re.MULTILINE | re.DOTALL)
	#print(matches.group())
	fileLinksList.append((sanitizeFileName(matches.group()), browser.get_link(class_ = "button").attrs['href']))
	#browser.follow_link(SubjectLink)# get back to page with lectures

print("Lectures found: {}".format(len(fileLinksList)))
if not os.path.isdir("{}\\{}".format(sys.argv[1],SubjectName)):
	os.makedirs("{}\\{}".format(sys.argv[1],SubjectName))

for link in fileLinksList:
	print('{}\\{}\\{}.mp4'.format(sys.argv[1],SubjectName, link[0]))
	#stáhnout
	downloadWithProgress(link[1],'{}\\{}\\{}.mp4'.format(sys.argv[1],SubjectName, link[0]))