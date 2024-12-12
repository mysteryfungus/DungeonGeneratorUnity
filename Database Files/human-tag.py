import re
import time

import requests
from bs4 import BeautifulSoup
import sqlite3

url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/creatures/index.html#creatures-bestiary"
base_url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/creatures/"


connect = sqlite3.connect("DungeonGenerator.db")
cursor = connect.cursor()

response = requests.get(url)
html = response.text
soup = BeautifulSoup(html, "html.parser")

npc_container = soup.select_one("section#creatures-npcs")

npcs = npc_container.find_all(lambda tag: tag.name == "a" and "Существо" in tag.text)


urls = []
for npc in npcs:
    url = npc["href"]
    urls.append(base_url + "/" + url)

for url in urls:
    time.sleep(0.01)
    response = requests.get(url)
    html = response.text
    soup = BeautifulSoup(html, "html.parser")
    tags = soup.find("section", {"class": "creature"}).find_next("ul")
    name = soup.find("h1").text
    cleaned_name = name.replace("¶", "")
    creature = re.search(r' / Существо \d+', cleaned_name)
    if creature is not None:
        erase_creature = creature.group()
        cleaned_name = cleaned_name.replace(erase_creature, "")
    tags = list(filter(lambda x: x != '\n', tags))

    if "Необычный" not in tags or "Редкий" not in tags or "Уникальный" not in tags:
        print("Тег: 'Обычный'")
        tag_id_row = cursor.execute("SELECT id FROM Tags WHERE Tag = ?", ("Обычный (Common)",)).fetchone()
        tag_id = tag_id_row[0] if tag_id_row else None
        print(f"Айди тега: '{tag_id}'")

        print(f"Человек: '{cleaned_name}'")
        human_id_row = cursor.execute("SELECT id FROM Humans WHERE Name = ?", (cleaned_name,)).fetchone()
        human_id = human_id_row[0] if human_id_row else None
        print(f"Айди человека: '{human_id}'\n")

        cursor.execute("INSERT INTO 'Humans-Tags' (Human_ID, Tag_ID) VALUES (?, ?)", (human_id, tag_id))

    for t in tags:
        tag_to_find = t.get_text().title()
        print(f"Тег: '{tag_to_find}'")
        tag_id_row = cursor.execute("SELECT id FROM Tags WHERE Tag LIKE ?", (tag_to_find + '%',)).fetchone()
        tag_id = tag_id_row[0] if tag_id_row else None
        print(f"Айди тега: '{tag_id}'")

        print(f"Человек: '{cleaned_name}'")
        human_id_row = cursor.execute("SELECT id FROM Humans WHERE Name = ?", (cleaned_name,)).fetchone()
        human_id = human_id_row[0] if human_id_row else None
        print(f"Айди человека: '{human_id}'\n")

        if tag_id is not None and human_id_row is not None:
            cursor.execute("INSERT INTO 'Humans-Tags' (Human_ID, Tag_ID) VALUES (?, ?)", (human_id, tag_id))
        else:
            print("Не удалось найти ID для тега или монстра")


connect.commit()
connect.close()



