# URLParser
A text file stores information line by line about URLs represented in the form
https://github.com/AnzhelikaKravchuk/.NET-Training-Materials/blob/master/Pictures/Scheme.png?raw=true
where the parameters segment is a set of key = value pairs, while the URL-path and parameters segments or the parameters segment may be missing. Develop a type system for exporting data obtained by parsing the information of a text file into an XML document according to the following rule, for example, for a text file with URLs

https://github.com/AnzhelikaKravchuk?tab=repositories (Links to an external site.)
https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU (Links to an external site.)
https://habrahabr.ru/company/it-grad/blog/341486/ (Links to an external site.)

https://github.com/AnzhelikaKravchuk/.NET-Training-Materials/blob/master/Pictures/XML.Task.png
the resultant is an XML document of the form (provide for the possibility of obtaining an XML document using any XML technology - XmlReader / XmlWriter, X-DOM).
