import re
import requests
from bs4 import BeautifulSoup
import sqlite3

url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/game_mastering/hazards.html#hazards-simple"
connect = sqlite3.connect("DungeonGenerator.db")
cursor = connect.cursor()

response = requests.get(url)

html = response.text
soup = BeautifulSoup(html, "html.parser")

complex_hazards_container = soup.select_one("section#hazards-complex.hazards-list")
complex_hazards = complex_hazards_container.find_all("section")
simple_hazards_container = soup.select_one("section#hazards-simple.hazards-list")
simple_hazards = simple_hazards_container.find_all("section")

args = []
tag_list = []

for hazard in complex_hazards:
    name = hazard.find_next("h2").text
    slash = re.search("/ Опасность [+-]?\d+", name).group()
    cleaned_name1 = name.replace("¶", "")
    cleaned_name2 = cleaned_name1.replace(slash, "")
    cleaned_name2 = cleaned_name2.rstrip()

    tags = hazard.find_next("ul")
    filtered_tags = list(filter(lambda x: x != '\n', tags))
    for t in filtered_tags:
        tag_to_find = t.get_text().title()
        print(f"Тег: '{tag_to_find}'")
        tag_id_row = cursor.execute("SELECT id FROM Tags WHERE Tag LIKE ?", (tag_to_find + '%',)).fetchone()
        tag_id = tag_id_row[0] if tag_id_row else None
        print(f"Айди тега: '{tag_id}'")

        print(f"Ловушка: '{cleaned_name2}'")
        hazard_id_row = cursor.execute("SELECT id FROM Hazards WHERE Name LIKE ?", (cleaned_name2 + '%',)).fetchone()
        hazard_id = hazard_id_row[0] if hazard_id_row else None
        print(f"Айди ловушки: '{hazard_id}'\n")

        if tag_id is not None and hazard_id_row is not None:
            cursor.execute("INSERT INTO 'Hazards-Tags' (Hazard_ID, Tag_ID) VALUES (?, ?)", (hazard_id, tag_id))
        else:
            print("Не удалось найти ID для тега или монстра")

for hazard in simple_hazards:
    name = hazard.find_next("h2").text
    slash = re.search("/ Опасность [+-]?\d+", name).group()
    cleaned_name1 = name.replace("¶", "")
    cleaned_name2 = cleaned_name1.replace(slash, "")
    cleaned_name2 = cleaned_name2.rstrip()

    tags = hazard.find_next("ul")
    filtered_tags = list(filter(lambda x: x != '\n', tags))
    for t in filtered_tags:
        tag_to_find = t.get_text().title()
        print(f"Тег: '{tag_to_find}'")
        tag_id_row = cursor.execute("SELECT id FROM Tags WHERE Tag LIKE ?", (tag_to_find + '%',)).fetchone()
        tag_id = tag_id_row[0] if tag_id_row else None
        print(f"Айди тега: '{tag_id}'")

        print(f"Ловушка: '{cleaned_name2}'")
        hazard_id_row = cursor.execute("SELECT id FROM Hazards WHERE Name LIKE ?", (cleaned_name2 + '%',)).fetchone()
        hazard_id = hazard_id_row[0] if hazard_id_row else None
        print(f"Айди ловушки: '{hazard_id}'\n")

        if tag_id is not None and hazard_id_row is not None:
            cursor.execute("INSERT INTO 'Hazards-Tags' (Hazard_ID, Tag_ID) VALUES (?, ?)", (hazard_id, tag_id))
        else:
            print("Не удалось найти ID для тега или монстра")

connect.commit()
connect.close()



