import time
import re
import requests
from bs4 import BeautifulSoup
import sqlite3
import pandas

# url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/creatures/index.html#creatures-bestiary"
# base_url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/creatures/"
#
# response = requests.get(url)
#
# html = response.text
# soup = BeautifulSoup(html, "html.parser")
#
# npc_container = soup.select_one("section#creatures-npcs")
#
# npcs = npc_container.find_all(lambda tag: tag.name == "a" and "Существо" in tag.text)
#
# urls = []
# for npc in npcs:
#     url = npc["href"]
#     urls.append(base_url + "/" + url)

url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/game_mastering/hazards.html#hazards-simple"

response = requests.get(url)

html = response.text
soup = BeautifulSoup(html, "html.parser")

complex_hazards_container = soup.select_one("section#hazards-complex.hazards-list")
complex_hazards = complex_hazards_container.find_all("section")
simple_hazards_container = soup.select_one("section#hazards-simple.hazards-list")
simple_hazards = simple_hazards_container.find_all("section")

tag_list = []
unique_tags = []

# for url in urls:
#     time.sleep(0.01)
#     response = requests.get(url)
#     html = response.text
#     soup = BeautifulSoup(html, "html.parser")
#     tags = soup.find("section", {"class": "creature"}).find_next("ul")
#     for t in tags:
#         tag_list.append((t.get_text().title(),))
#     tag_list = list(filter(lambda x: x != '\n', tag_list))
#     name = soup.find("h1").text
#     cleaned_name = name.replace("¶", " ")
#     print(cleaned_name)

for hazard in complex_hazards:
    name = hazard.find_next("h2").text
    slash = re.search("/ Опасность [+-]?\d+", name).group()
    cleaned_name1 = name.replace("¶", "")
    cleaned_name2 = cleaned_name1.replace(slash, "")

    tags = hazard.find_next("ul")
    for t in tags:
        if t != "\n":
            tag_list.append((t.get_text().title(),))

    #print(cleaned_name2)
#print(tag_list)

for hazard in simple_hazards:
    name = hazard.find_next("h2").text
    slash = re.search("/ Опасность [+-]?\d+", name).group()
    cleaned_name1 = name.replace("¶", "")
    cleaned_name2 = cleaned_name1.replace(slash, "")

    tags = hazard.find_next("ul")
    for t in tags:
        if t != "\n":
            tag_list.append((t.get_text().title(),))
    #print(cleaned_name2)

#connect = sqlite3.connect("DungeonGenerator.db")

#cursor = connect.cursor()

#cursor.executemany("INSERT INTO Hazards (Name, Complexity, Description, MechDescription, Level, Stealth) VALUES (?, ?, ?, ?, ?, ?)", args)
#connect.commit()
#connect.close()

unique_tags = pandas.Series(tag_list).drop_duplicates().tolist()
unique_tags.pop(6)
unique_tags.pop(20)
print(unique_tags)

connect = sqlite3.connect("DungeonGenerator.db")

cursor = connect.cursor()

cursor.executemany("INSERT INTO Tags (Tag) VALUES (?)", unique_tags)

connect.commit()
connect.close()

