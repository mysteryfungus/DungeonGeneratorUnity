import re
import requests
from bs4 import BeautifulSoup
import sqlite3
import pandas

url = "https://pf2e-ru-translation.readthedocs.io/ru/latest/game_mastering/hazards.html#hazards-simple"

response = requests.get(url)

html = response.text
soup = BeautifulSoup(html, "html.parser")

hazard_container = soup.select_one("section#hazards-complex.hazards-list")
hazards = hazard_container.find_all("section")
args = []

for hazard in hazards:
    name = hazard.find_next("h2").text
    slash = re.search("/ Опасность [+-]?\d+", name).group()
    cleaned_name1 = name.replace("¶", "")
    cleaned_name2 = cleaned_name1.replace(slash, "")

    complexity = "Простая"

    level = re.search(r'[+-]?\d+', name).group()

    description = hazard.find(lambda tag: tag.name == 'p' and 'Описание' in tag.text).text
    cleaned_descr = description.replace('Описание: ', " ")

    section_id = hazard.get('id')
    mech_description = hazard.find('hr').find_next_siblings('p')
    mech_list = []
    for tag in mech_description:
        cleaned_mech_descr = tag.text
        mech_list.append(cleaned_mech_descr)
    cleaned_mech_list = '\n'.join(mech_list)

    stealth = hazard.find(lambda tag: tag.name == 'p' and 'Скрытность' in tag.text).text
    cleaned_stealth = stealth.replace("Скрытность", " ")

    print(cleaned_name2)
    args.append((cleaned_name2, complexity, cleaned_descr, cleaned_mech_list, level, stealth))

print(len(hazards))
#connect = sqlite3.connect("DungeonGenerator.db")

#cursor = connect.cursor()

#cursor.executemany("INSERT INTO Hazards (Name, Complexity, Description, MechDescription, Level, Stealth) VALUES (?, ?, ?, ?, ?, ?)", args)
#connect.commit()
#connect.close()

