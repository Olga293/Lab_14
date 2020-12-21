using System;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;

namespace lab_14_1_
{
    [Serializable]
    public abstract class GeneralInfo
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Cover { get; set; }
    }
    [Serializable]
    public class Book : GeneralInfo
    {
        public Book() { }
        public Book(string title, string country, int year, int pages, string cover)
        {
            Title = title;
            Country = country;
            Year = year;
            Pages = pages;
            Cover = cover;
        }
        public override string ToString()
        {
            return "~~~~~~~~~~Information about book~~~~~~~~~~\nTitle: " + Title + "\nYear: " + Year + "\nPages: " + Pages + "\nCountry: " + Country;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("Harry Potter and the Prisoner of Azkaban", "United Kingdom", 1999, 464, "hard");
            Book book2 = new Book("Harry Potter and the Half-Blood Prince", "United Kingdom", 2005, 607, "hard");
            Book book3 = new Book("Harry Potter and the Philosopher's Stone", "United Kingdom", 1997, 332, "hard");
            Book book4 = new Book("Harry Potter and the Goblet of Fire", "United Kingdom", 2000, 636, "hard");



            Console.WriteLine("~~~~~~~~~ Binary serialization ~~~~~~~~~");
            BinaryFormatter binary= new BinaryFormatter();
            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.dat", FileMode.OpenOrCreate))
            {
                binary.Serialize(fstream, book1);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.dat", FileMode.OpenOrCreate))
            {
                Book newbook1 = (Book)binary.Deserialize(fstream);
                Console.WriteLine(newbook1.ToString() + "\n");
            }



            Console.WriteLine("\n~~~~~~~~~ Soap serialization ~~~~~~~~~");
            SoapFormatter soap = new SoapFormatter();
            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.soap", FileMode.OpenOrCreate))
            {
                soap.Serialize(fstream, book2);
            }

            using (FileStream fs = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.soap", FileMode.OpenOrCreate))
            {
                Book newbook2 = (Book)soap.Deserialize(fs);
                Console.WriteLine(newbook2.ToString() + "\n");
            }



            Console.WriteLine("\n~~~~~~~~~ Json serialization ~~~~~~~~~");
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Book));
            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.json", FileMode.OpenOrCreate))
            {
                json.WriteObject(fstream, book3);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.json", FileMode.OpenOrCreate))
            {
                Book newbook3 = (Book)json.ReadObject(fstream);
                Console.WriteLine(newbook3.ToString() + "\n");
            }



            Console.WriteLine("\n~~~~~~~~~ XML serialization ~~~~~~~~~");
            XmlSerializer xml = new XmlSerializer(typeof(Book));
            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fstream, book4);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\book.xml", FileMode.OpenOrCreate))
            {
                Book newbook4 = (Book)xml.Deserialize(fstream);
                Console.WriteLine(newbook4.ToString() + "\n");
            }



            Book[] books = new Book[] { book1, book2, book3, book4 };

            Console.WriteLine("\n~~~~~~~~~ XML serialization of objects array ~~~~~~~~~");
            XmlSerializer masxml = new XmlSerializer(typeof(Book[]));
            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\bookmas.xml", FileMode.OpenOrCreate))
            {
                masxml.Serialize(fstream, books);
            }

            using (FileStream fstream = new FileStream(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\bookmas.xml", FileMode.OpenOrCreate))
            {
                Book[] newbooks = (Book[])masxml.Deserialize(fstream);
                foreach (Book x in newbooks)
                {
                    Console.WriteLine(x.ToString() + "\n");
                }
            }



            Console.WriteLine("\n~~~~~~~~~ XPath ~~~~~~~~~");
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\bookmas.xml");
            XmlElement element = document.DocumentElement;
            XmlNodeList nodes = element.SelectNodes("Book[Year='2000']"); // все эл-ты с определенным значением вложенного эл-та
            if (nodes != null)
            {
                foreach (XmlNode n in nodes)
                {
                    Console.WriteLine(n.OuterXml + "\n");
                }
            }
            XmlNode elem = element.SelectSingleNode("Book[4]"); // выбор по индексу
            if (elem != null)
            {
                Console.WriteLine(elem.OuterXml);
            }



            Console.WriteLine("\n~~~~~~~~~ Linq to XML ~~~~~~~~~");
            XDocument xmlfile = new XDocument();
            XElement head = new XElement("books");

            XElement elem1 = new XElement("book");
            XAttribute name1 = new XAttribute("name", "Harry Potter and the Philosopher's Stone");
            XElement year1 = new XElement("year", "1997");
            XElement country1 = new XElement("country", "United Kingdom");
            elem1.Add(name1);
            elem1.Add(year1);
            elem1.Add(country1);

            XElement elem2 = new XElement("book");
            XAttribute name2 = new XAttribute("name", "Harry Potter and the Prisoner of Azkaban");
            XElement year2 = new XElement("year", "1999");
            XElement country2 = new XElement("country", "United Kingdom");
            elem2.Add(name2);
            elem2.Add(year2);
            elem2.Add(country2);

            XElement elem3 = new XElement("book");
            XAttribute name3 = new XAttribute("name", "Harry Potter and the Goblet of Fire");
            XElement year3 = new XElement("year", "2000");
            XElement country3 = new XElement("country", "United Kingdom");
            elem3.Add(name3);
            elem3.Add(year3);
            elem3.Add(country3);

            XElement elem4 = new XElement("book");
            XAttribute name4 = new XAttribute("name", "Harry Potter and the Half-Blood Prince");
            XElement year4 = new XElement("year", "2005");
            XElement country4 = new XElement("country", "United Kingdom");
            elem4.Add(name4);
            elem4.Add(year4);
            elem4.Add(country4);

            head.Add(elem1);
            head.Add(elem2);
            head.Add(elem3);
            head.Add(elem4);

            xmlfile.Add(head);
            xmlfile.Save(@"C:\Users\Olga\OneDrive\Documents\BSTU\2_course\1_semester\OOP\lab_14\Lab_14\xmlfile.xml");

            Console.WriteLine("LINQ-1: ");
            var linq1 = xmlfile.Element("books").Elements("book").Where(n => n.Attribute("name").Value == "Harry Potter and the Half-Blood Prince");
            foreach (var x in linq1)
            {
                Console.WriteLine(x + "\n");
            }

            Console.WriteLine("LINQ-2: ");
            var linq2 = from x in xmlfile.Element("books").Elements("book")
                        orderby (x.Element("year").Value)
                        select x;
            foreach (var x in linq2)
            {
                Console.WriteLine(x + "\n");
            }
        }
    }
}
