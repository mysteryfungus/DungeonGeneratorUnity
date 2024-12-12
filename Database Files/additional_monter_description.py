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

connect = sqlite3.connect("DungeonGenerator.db")
cursor = connect.cursor()

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
    print(cleaned_name)

    description = soup.find("h1").find_next_siblings("p")
    cleaned_descr = " ".join(p.get_text() for p in description)

    additional_description = soup.find_all("aside", {"class": "sidebar"})
    if additional_description is not None:
        cleaned_adescr = " ".join(p.get_text() for p in additional_description)
        full_description = cleaned_descr + "\n" + cleaned_adescr
        cursor.execute("INSERT INTO Monsters (Description) VALUES (?)", (full_description,))
    else:
        cursor.execute("INSERT INTO Monsters (Description) VALUES (?)", (description,))

connect.commit()
connect.close()
