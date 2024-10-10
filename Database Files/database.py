import sqlite3

connect = sqlite3.connect("DungeonGenerator.db")

table_query = ("CREATE TABLE Monsters (id INTEGER NOT NULL PRIMARY KEY, Name TEXT NOT NULL, Description "
               "TEXT NOT NULL, MechDescription TEXT NOT NULL, Level INTEGER NOT NULL)")
cursor = connect.cursor()

cursor.execute(table_query)
connect.close()
