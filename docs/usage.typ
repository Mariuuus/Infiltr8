#import "lib.typ" : game_design_document

#show: game_design_document.with(
  //title: 
  title: "Infiltr8",
  //authors:
  authors: ("Marius Christoph Strauss", "Maximilian Carl Ferdinand Schmitt", "Thomas Eichmann"),
)

= Vision Statement
// "The Game in a Tweet" -- maximal ein oder zwei Sätze, die das Spiel High-Level beschreiben.
Aus einem Kinderzimmer wird die Hackerzentrale. Als Hacker -- in der Ausbildung -- geht darum unbemerkt in Systeme einzudringen, diese zu verstehen, Schwachstellen auszunutzen mit dem Ziel geheime Daten zu klauen.

= Genre
== Puzzle
Das Hauptelement unseres Spiels sind Puzzleelemente. Ein Puzzle ist dabei immer durch ein Level realisiert. Diese Level variieren in der Komplexität und können somit auch wieder Teilrätsel enthalten. Das Thema _Hacken_ haben wir durch 4 Farben (#text([rot], red), #text([grün], green), #text([blau], blue), #text([gelb], yellow)) dargestellt, welche verschiedene Hackmethoden darstellen sollen. Gehackte bzw. gefärbte Laptops dienen dann als Schlüssel um Schwachstellen in Firewalls auszunutzen.

== Strategy
Der Spieler muss selber Strategien entwickeln, um Puzzle zu lösen. Besonders Level, in welchen eine zeitbasierte Komponent hinzukommen. Dabei sind die Rätsel nicht linear lösbar, sonderen können auch auf verschiedene Arten gelöst werden. Der Spieler muss somit teilweise zunächst komplexe Strategien entwickeln, um Level abzuschließen.

== Sandbox

Unsere Spiel beinhaltet auch einen eigenen Leveleditor, welchen wir nutzen konnten, um Level für die Kampagne zu erstellen. Dem normalen Spieler haben wir jedoch auch die Möglichkeit eröffnet den Leveleditor zu nutzen. Dabei können die Spieler dann selber kreativ werden und zudem gibt es die Möglichkeit Level direkt innerhalb des Spiels hoch- und herunterzuladen. So können Spieler dann ihre kreativen Levelideen mit anderen Spielern teilen.

= Zielpublikum 
/* Geben Sie hier Motivationen und relevante Interessen des Zielpublikums an; möglicherweise Alter, Geschlecht usw. Gewünschte Alterseinstufung 
 */
Unser Spiel richtet sich an Spieler, welche Interesse an der eigenständigen Lösungfindung haben un der Interesse an der Gamification des Thema "Cyber-Security". Das Alter oder Geschlecht ist für die Spieler unseres Spiel nicht von relevanz. Unser Spiel benötigt daher auch keine Altersbeschrenkung.


= Features 

- Abstraktion des -- meist langwierigen -- Prozesses des Hackens hinzu der Verwendung von Farben als Hackarten

- 

/* Geben Sie hier die wichtigsten Features (Alleinstellungsmerkmale) Ihres Spiels an. 

- Merkmal 1 

- Merkmal 2  */

= Gameplay 

//Beschreiben Sie hier alles, was zum Gameplay gehört.  

== Objectives 

=== Hauptziel

Das Hauptziel unseres Spiel ist das erfolgreiche Abschließen  aller existierenden Level der Kampange. 

=== Nebenziele

==== Collectibles

In den vorgegeben Level kann der Spieler zudem _Collectibles_ einsammeln. Dabei handelt es sich um ein optionales Ziel. In dem Spiel kann der Spieler 12 _Collectibles_ sammeln.

==== Speedruns

Ein dedizierter Speedrunmodus ermöglich es den Spielern alle vorgegeben Level direkt hintereinander zu spielen. Die Spieler können dann versuchen einen möglichst geringe Bestzeit aufzustellen.

// Welche Ziele werden vom game Design in Ihrem Spiel vorgegeben bzw. welche Ziele müssen Spieler erfüllen, um das Spiel abzuschließen. 

== Core Gameplay Loop 

#figure(image("res/gameloop.png"), caption: [Core Gameplay Loop ])

Der *Core Gameplay Loop* besteht aus dem folgenden drei Elementen *Understand*, *Hack*, *Solve*. Der Spieler muss beim Spielen eines Levels meist mehrfach diese drei Phasen durchgehen, da Level meist aus mehreren kleiner Rätseln bestehen. 

In der *Understand*-Phase muss der Spieler zunächst ein Überblick über das vorliegene Teilrätsel erlangen. Dabei kann es sich um die Existenz von _Laptops_ mit Einschränkungen, der Position von _Firewalls_ und _Activation Plates_ u.Ä. handeln. 

In der *Hack*-Phase wird der Spieler dann ggf. die einzelne Farbe der Laptops ändern, Laptops durch Portale bewegen oder Laptops an bestimmte Stellen bewegen. 

Dadurch geht der Spieler dann in die *Solve*-Phase über. Das Teilrätsel ist dann gelöst. Es kann hierbei noch zu weiteren Handlungen, wie das erneute bewegen von Laptops o.Ä. kommen. 

Nun ist das Level enweder beendet und der Spieler geht in das nächste Level über oder der Spieler muss zunächst die nächsten Teilrätsel lösen.

/* Die Core Gameplay Loop des Spiels kann als die zentrale Spielmechanik oder Abfolge von Aktionen definiert werden, an denen Spieler wiederholt teilnehmen, um innerhalb eines Spiels voranzukommen und Ziele zu erreichen. Sie umfasst die grundlegendsten Arten von Aktionen, die Spieler ausführen können. Beispiel für einen Shooter: aim, fire, advance, repeat Stellen Sie das am besten grafisch dar.  */

== Mechaniken 

#figure(
  table(columns: 3, inset:8pt,
    table.header(
      [*Kernmechanik*],[*Untermechanik*],[*Beschreibung*]
    ),
    align: (horizon, horizon, left),
    table.cell([*Hacken*], rowspan: 2, align: horizon),
    [_Farbwechsel_], [Der Spieler hat die Möglichkeit Laptops zu hacken. Dabei kann er die Laptops in eine maximal vier Farben (#text([rot], red), #text([grün], green), #text([blau], blue), #text([gelb], yellow)) ändern.],
    [_Firewalls_/_Activation Plates_], [Eine Firewall ist eine dem Wand, welche den Zustand offen oder zu annehmen kann. 
    
    Um eine Firewall zu öffnen, müssen die Vorrausesetzungen erfüllt werden. Die Vorraussetzungen bestehen hierbei darin bestimmte Anzahlen von gefärbten Laptops zu den zugehlörigen Aktivation Plates der Firewall zu bringen. Die Firewall ist dann nur solange offen, wie die Vorraussetzungen erfüllt sind.
    
    Eine Aktivation Plate hat dabei ein Limit an maximal belegbaren Laptops.],

    table.cell([*Netzwerkbewegung*], rowspan: 3, align: horizon),
    [_Spielerbewegung_], [Eine Spielfigur kann durch den Spieler durch das Netzwerk (Level navigiert werden)],
    [_Laptop tragen_], [Der Spieler kann immer genau einen Laptop tragen, um diesen an bestimmte Orte zu bringen],
    [_Portale_], [Es sind immer zwei Portale miteinander verbunden. Der Spieler kann dann Laptops durch diese Portale zum jeweils anderen Portal teleportieren.],

    table.cell([*Blockierungen*], rowspan: 2, align: horizon),
    [_Vulnerabilies_], [Eine Wand, welche nur von dem Spieler, nicht aber von Laptops durchgangen werden kann.],
    [_Decorations_], [Dekorationen, welchen keinen Nutzen für die Spiellogik haben, außer den Spieler ggf. bei der Navigation zu stören.],

    table.cell([*Zeit*], rowspan: 2, align: horizon),
    [_Limit_], [Es kann ein Zeitlimit ($x$ Sekunden) pro Level geben. Der Spieler muss das Level dann in der gegebenen Zeit beenden, sonst kommt es zu einem _Reset_.],
    [_Defenders_], [stetig bewegende Elemente, welche bei der Berührung mit dem Spieler diesen den haltenden Gegenstand fallen lassen. Zudem wird der Spieler für einige Zeit verlangsamt. 
    Berührt ein Defender einen Laptop, so wird dieser zurückgesetzt. Der Laptop ist dann nicht mehr gehackt.],
    ), caption: [Mechaniken]
)

/* Welche Mechaniken werden in Ihrem Spiel genutzt. Geben Sie hier eine tabellarische Aufstellung an. Eventuell teilen Sie die Mechaniken auch in Kern- und aufbauende Mechaniken und geben an, wie sie aufeinander aufbauen.  */

== Ressourcen / Items 

Welche Ressourcen verwendet Ihr Spiel? Welche Items gibt es … tabellarische Aufstellung mit Verwendung(szweck) des Items / der Ressource? 

== ... eventuell weitere Abschnitte 

= Kontrollen / Menüs 

Wie erfolgt die Interaktion mit dem Spiel? Listen Sie hier alle Aktionen (Tastatur/Maus/Controller) auf, die Spieler durchführen, um das Spiel zu spielen. Sie können auch eine Grafik der Tastenbelegung des Controllers z.B. einfügen.  

Stellen Sie dar, wo sich welche Kontrollelemente auf dem Bildschirm befinden (Skizze) 

Welche Menüs gibt es im Spiel (Skizze) 

= Narrativ 

Wenn es eine Story in Ihrem Spiel gibt, so besteht dieser Abschnitt daraus, die Story und den Hintergrund des Spiels darzustellen. 

== Story 

Welche Geschichte liegt Ihrem Spiel zu Grunde? 

== Spielwelt 

Wie ist die Spielwelt aufgebaut? Welche Regeln gelten dort? Welche Naturgesetze? Welche politischen Gesetze, … Geben Sie Karten an. 

== Charaktere 

Beschreiben Sie hier die einzelnen Charaktere ausführlich (wenn nötig auch mit allen Statuswerten) 

= Art-Style 

Beschreiben Sie hier den Art-Style Ihres Spiels, beispielsweise durch Moodboards oder Beispiele von anderen Spielen. Oder Sie binden hier Concept-Art Ihres Spiels ein. 

= Team und Technologie 

#align(center, 
  table(columns: 2, inset: 10pt,
  [*Name*],[*Rolle*],
  [Felix Musterperson],[Game Design, 3D Arts],
  )
)

== Verwendete Engine:	

Unity

== Verwendete Tools: 
- #link("https://www.blender.org/")[Blender]: Zur Erstellung von Modellen, Texturen und Animationen.
- #link("https://www.gimp.org/downloads/")[GIMP]: Zur Erstellenung von Grafiken, Logos, Icons u.Ä.
- #link("https://www.osamc.de/archiv/sfxr/")[SFXR]: Zur Generierung von Soundeffekten.
- #link("https://gitlab.hs-anhalt.de/")[Gitlab der Hochschule]: Zur Versionskontrolle und als Registry.



== KI-Werkzeuge wurden wie folgt verwendet: 
- Texturen generieren 
- Code generieren 