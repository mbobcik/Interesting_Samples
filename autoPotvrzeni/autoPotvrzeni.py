#!/usr/bin/env python3
import json
from requests import Session
import requests
from robobrowser import RoboBrowser
from bs4 import BeautifulSoup
import os
# surpress warnings, mainly due to no certificates
import warnings
warnings.filterwarnings("ignore")

infoText = "Dnes nebylo podepsáno prohlášení o neexistenci příznaků virového onemocnění."

__location__ = os.path.realpath(os.path.join(os.getcwd(), os.path.dirname(__file__)))

config = json.load(open(os.path.join(__location__,'autoPotvrzeni.config.json')))

# create new session disabling certificate checking
session = Session()
session.verify = False
browser = RoboBrowser(session=session)

# log in to VUTBR Studis
browser.open('https://www.vutbr.cz/login/intra')

print(browser.find('title').next)

form = browser.get_form(id='login_form')
form['LDAPlogin'].value = config["login"]
form['LDAPpasswd'].value = config["password"]
browser.submit_form(form)

print(browser.find('title').next)
browser.open("https://www.vutbr.cz/studis/student.phtml?sn=prohlaseni_studenta")
print(browser.find('title').next)
if browser.parsed.text.find(infoText) != -1:
    browser.open("https://www.vutbr.cz/studis/student.phtml?sn=prohlaseni_studenta&podepsat=2")
    print("Just Confirmed, you can go to school, if you wish!")
else:
    print("Already Confirmed! Why are you bullying me?!")