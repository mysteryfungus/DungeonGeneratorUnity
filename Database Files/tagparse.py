import time

import requests
from bs4 import BeautifulSoup
import sqlite3
import pandas

url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/creatures/index.html#creatures-bestiary"
base_url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/creatures/"

response = requests.get(url)

html = response.text
soup = BeautifulSoup(html, "html.parser")

monster_container = soup.select_one("section#creatures-bestiary")

bestiary1 = monster_container.find_all(lambda tag: tag.name == "a" and "Существо" in tag.text)

urls = []
for beast in bestiary1:
    url = beast["href"]
    urls.append(base_url + "/" + url)

args = []
tag_list = []
unique_tags = []

for url in urls:
    time.sleep(0.01)
    response = requests.get(url)
    html = response.text
    soup = BeautifulSoup(html, "html.parser")
    tags = soup.find("section", {"class": "creature"}).find_next("ul")
    for t in tags:
        tag_list.append((t.get_text().title(),))
    tag_list = list(filter(lambda x: x != '\n', tag_list))
    name = soup.find("h1").text
    cleaned_name = name.replace("¶", " ")
    print(cleaned_name)

unique_tags = pandas.Series(tag_list).drop_duplicates().tolist()
print(unique_tags)
unique_tags.pop(0)

connect = sqlite3.connect("DungeonGenerator.db")

cursor = connect.cursor()

cursor.executemany("INSERT INTO Tags (Tag) VALUES (?)", unique_tags)

connect.commit()
connect.close()

