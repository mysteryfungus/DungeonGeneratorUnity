import requests
import re
from bs4 import BeautifulSoup
import sqlite3
import pandas

url = 'https://pf2e-ru-translation.readthedocs.io/ru/latest/appendix/glossary_index.html#appendix-traits-other'

response = requests.get(url)
html = response.text
soup = BeautifulSoup(html, "html.parser")

tag_description_container = soup.select_one("section#other-traits").find_all("section")

args = []
description_list = []

connect = sqlite3.connect("DungeonGenerator.db")
cursor = connect.cursor()

for tag in tag_description_container:
    name = tag.find('h4').text
    name = re.sub(r'\([^)]*\)', '', name)
    name = name.replace("¶", "").replace(")", "")
    name = re.sub(r'/.*', '', name).strip()
    tag_in_db = cursor.execute("SELECT Tag FROM Tags WHERE Tag LIKE ?", (name + '%',)).fetchone()
    print(f'Найденное имя на сайте - {name}\nНайденное имя в бд - {tag_in_db}\n')
    if tag_in_db is not None:
        description = tag.find('p').text
        description = description.replace('\n', "").strip()
        cursor.execute("UPDATE Tags SET TagDescription = ? WHERE Tag LIKE ?", (description, name + '%'))

connect.commit()
connect.close()

