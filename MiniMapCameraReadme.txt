Zur Erschaffung einer neuen Kamera f�r die Minimap,
im geplanten Spiel die Vogelperspektive des Schiffs,
muss man �ber creatae zuerst eine neue Kamera einf�gen.

�ber "Solid Color" im Clear Flags kann man den normalen Hintergrund,
bzw. die Hintergrundtextur ausblenden und eine einzige Farbe ausw�hlen.

�ber Culling Mask kann man die durch die Kamera zu rendernden Objekte ausw�hlen.
Wenn man z.B. nur ein Schiff rendern lassen will, muss man dsa Schick mit einem
Layer belegt werden, der z.B. "Schiff" hei�t und im angesprochenen Culling Mask
muss nur "Schiff" ausgw�hlt werden. Es ist hierbei m�glich erst
alles zu desesektieren, �ber "Nothing" als erste Option.

Die Position der eigentlichen Darstellung der Kamera ist �ber Viewport Rect
einstellbar. mit X und Y lassen sich die Ausf�llung des Bildsschirms mit der
Kamera ausw�hlen. Wir w�hlen hier erstmal 0.6.

�ber Depth l�sst sich auch was einstellen, kleinere Werte vor gr��eren Werten,
vielleicht das R�ndern oder so.