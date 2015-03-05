using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Domain
{

    [Serializable]
    public class JsonObject
    {

        public int Count;
        public int[] Ids;
        public Record[] Records;

        //Empty Constructor
        public JsonObject() { }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static JsonObject FromJson(string json)
        {
            return JsonConvert.DeserializeObject<JsonObject>(json);
        }
    }


    [Serializable]
    public class Field
    {

        public string[] Values;
        public int NumberOfValues;
        public int[] ValuesAsInteger;
        public bool[] ValuesAsBoolean;
        public string ExtId;
        public int Id;

        //Empty Constructor
        public Field() { }

    }


    [Serializable]
    public class Type
    {

        public string ExtId;
        public int Id;

        //Empty Constructor
        public Type() { }

    }


    [Serializable]
    public class FirstField
    {

        public string[] Values;
        public int NumberOfValues;
        public int[] ValuesAsInteger;
        public bool[] ValuesAsBoolean;
        public string ExtId;
        public int Id;

        //Empty Constructor
        public FirstField() { }

    }


    [Serializable]
    public class Record
    {

        public Field[] Fields;
        public int Id;
        public Type Type;
        public string Values;
        public object[] Categories;
        public int TypeId;
        public FirstField FirstField;
        public bool Splitter;
        public string IdAndType;
        public string Ident;
        public bool MyObject;

        //Empty Constructor
        public Record() { }

    }

}