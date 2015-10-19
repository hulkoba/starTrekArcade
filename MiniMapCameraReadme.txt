Zur Erschaffung einer neuen Kamera für die Minimap,
im geplanten Spiel die Vogelperspektive des Schiffs,
muss man über creatae zuerst eine neue Kamera einfügen.

Über "Solid Color" im Clear Flags kann man den normalen Hintergrund,
bzw. die Hintergrundtextur ausblenden und eine einzige Farbe auswählen.

Über Culling Mask kann man die durch die Kamera zu rendernden Objekte auswählen.
Wenn man z.B. nur ein Schiff rendern lassen will, muss man dsa Schick mit einem
Layer belegt werden, der z.B. "Schiff" heißt und im angesprochenen Culling Mask
muss nur "Schiff" ausgwählt werden. Es ist hierbei möglich erst
alles zu desesektieren, über "Nothing" als erste Option.

Die Position der eigentlichen Darstellung der Kamera ist über Viewport Rect
einstellbar. mit X und Y lassen sich die Ausfüllung des Bildsschirms mit der
Kamera auswählen. Wir wählen hier erstmal 0.6.

Über Depth lässt sich auch was einstellen, kleinere Werte vor größeren Werten,
vielleicht das Rändern oder so.