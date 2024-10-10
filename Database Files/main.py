import re
import requests
from bs4 import BeautifulSoup
import sqlite3

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
    response = requests.get(url)
    html = response.text
    soup = BeautifulSoup(html, "html.parser")

    name = soup.find("h1").text
    cleaned_name = name.replace("¶", " ")

    description = soup.find("h1").find_next_siblings("p")
    cleaned_descr = " ".join(p.get_text() for p in description)

    mech_description = soup.find("section", {"class": "creature"}).text
    mech_description_name = soup.find("section", {"class": "creature"}).find_next(re.compile(r"h\d")).text
    no_name = mech_description.replace(mech_description_name, "")
    tags = soup.find("section", {"class": "creature"}).find_next("ul").text
    cleaned_mech = no_name.replace(tags, "")

    level = re.search(r'[+-]?\d+', mech_description_name).group()

    print(cleaned_name, cleaned_descr, cleaned_mech, level)
    args.append((cleaned_name, cleaned_descr, cleaned_mech, level))

connect = sqlite3.connect("DungeonGenerator.db")

cursor = connect.cursor()

cursor.executemany("INSERT INTO Monsters (Name, Description, MechDescription, Level) VALUES (?, ?, ?, ?)", args)
connect.commit()
сonnect.close()
