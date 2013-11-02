﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace SchetsEditor
{
    public class ObjectManager
    {
        private List<DrawObject> objects = new List<DrawObject>();

        public List<DrawObject> getObjects
        {
            get { return objects; }
        }

        public void SerializeToXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<DrawObject>));
            StreamWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, this.objects);
            writer.Close();
        }

        public void DeserializeFromXML(string filename)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<DrawObject>));
            TextReader textReader = new StreamReader(filename);
            this.objects = (List<DrawObject>)deserializer.Deserialize(textReader);
            textReader.Close();
        }

        public void assignObject(DrawObject tekenObject)
        {
            objects.Add(tekenObject);
        }
    }

    public class DrawFromXML
    {
        public static void DrawingFromXML(Graphics gr, List<DrawObject> objects)
        {
            Font font = new Font("Tahoma", 40);

            foreach (DrawObject obj in objects)
            {
                Color color = Color.FromName(obj.Color);
                SolidBrush brush = new SolidBrush(color);

                switch (obj.Tool)
                {
                    case "tekst":
                        gr.DrawString(obj.Text, font, brush, obj.Points[0], StringFormat.GenericTypographic);
                        break;
                    case "kader":
                        new RechthoekTool().Teken(gr, obj.Points[0], obj.Points[1], brush);
                        break;
                    case "vlak":
                        new VolRechthoekTool().Teken(gr, obj.Points[0], obj.Points[1], brush);
                        break;
                    case "cirkel":
                        new CirkelTool().Teken(gr, obj.Points[0], obj.Points[1], brush);
                        break;
                    case "rondje":
                        new RondjeTool().Teken(gr, obj.Points[0], obj.Points[1], brush);
                        break;
                    case "lijn":
                        new LijnTool().Teken(gr, obj.Points[0], obj.Points[1], brush);
                        break;
                    case "pen":
                        new PenTool().TekenLijn(gr, obj.Points, brush);
                        break;
                }
            }
        }
    }

    public class DrawObject
    {
        public DrawObject()
        { Text = null; }

        public string Tool
        { get; set; }

        public List<Point> Points { get; set; }

        public string Color
        { get; set; }

        public string Text
        { get; set; }

        
    }
}
