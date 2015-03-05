using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Domain;
using System.Collections.Generic;

namespace Scheduling.Tests
{
    [TestClass]
    public class JsonObjParserTest
    {
        JsonObjParser parser;
        [TestInitialize]
        public void TestInitialize()
        {
            parser = new JsonObjParser();
        }

        [TestMethod]
        public void TestInitialized()
        {
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void TestParseSimple()
        {
            parser.parseText(@"{""count"":0, ""ids"":[], ""records"":[]}");
            Assert.AreEqual(0, parser.ObjectCount);
        }

        [TestMethod]
        public void TestParseOneCourse()
        {
            string action = @"{""count"":3, ""ids"":[174563,189540,-1], ""records"":[{""fields"":[{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":174563,""type"":{""extId"":"""",""id"":199},""values"":""20743, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""174563.199"",""ident"":""174563.199"",""myObject"":false},{""fields"":[{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":189540,""type"":{""extId"":"""",""id"":199},""values"":""22756, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""189540.199"",""ident"":""189540.199"",""myObject"":false},{""fields"":[{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0}],""id"":-1,""type"":{""extId"":"""",""id"":0},""values"":""–"",""categories"":[],""typeId"":0,""firstField"":{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0},""splitter"":true,""idAndType"":""-1"",""ident"":""-1"",""myObject"":false}]}";
            List<Application> result = new List<Application>();
            result = parser.ParseJson(action);
            Assert.AreEqual(2, result.Count);
            
        }

        [TestMethod]
        public void TestParserApplicationCode()
        {
            string action = @"{""count"":3, ""ids"":[174563,189540,-1], ""records"":[{""fields"":[{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":174563,""type"":{""extId"":"""",""id"":199},""values"":""20743, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""174563.199"",""ident"":""174563.199"",""myObject"":false},{""fields"":[{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":189540,""type"":{""extId"":"""",""id"":199},""values"":""22756, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""189540.199"",""ident"":""189540.199"",""myObject"":false},{""fields"":[{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0}],""id"":-1,""type"":{""extId"":"""",""id"":0},""values"":""–"",""categories"":[],""typeId"":0,""firstField"":{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0},""splitter"":true,""idAndType"":""-1"",""ident"":""-1"",""myObject"":false}]}";
            List<Application> result = new List<Application>();
            result = parser.ParseJson(action);
            Assert.AreEqual(20743, result[0].Code);
        }

        [TestMethod]
        public void TestParserCourseCode()
        {
            string action = @"{""count"":3, ""ids"":[174563,189540,-1], ""records"":[{""fields"":[{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":174563,""type"":{""extId"":"""",""id"":199},""values"":""20743, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""174563.199"",""ident"":""174563.199"",""myObject"":false},{""fields"":[{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":189540,""type"":{""extId"":"""",""id"":199},""values"":""22756, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""189540.199"",""ident"":""189540.199"",""myObject"":false},{""fields"":[{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0}],""id"":-1,""type"":{""extId"":"""",""id"":0},""values"":""–"",""categories"":[],""typeId"":0,""firstField"":{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0},""splitter"":true,""idAndType"":""-1"",""ident"":""-1"",""myObject"":false}]}";
            List<Application> result = new List<Application>();
            result = parser.ParseJson(action);
            Assert.AreEqual(20743, result[0].Code);
            Assert.AreEqual("DVGC22", result[0].CourseCode);
        }
        [TestMethod]
        public void TestParserCourseName()
        {
            string action = @"{""count"":3, ""ids"":[174563,189540,-1], ""records"":[{""fields"":[{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":174563,""type"":{""extId"":"""",""id"":199},""values"":""20743, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""20743""],""numberOfValues"":1,""valuesAsInteger"":[20743],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""174563.199"",""ident"":""174563.199"",""myObject"":false},{""fields"":[{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":189540,""type"":{""extId"":"""",""id"":199},""values"":""22756, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""189540.199"",""ident"":""189540.199"",""myObject"":false},{""fields"":[{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0}],""id"":-1,""type"":{""extId"":"""",""id"":0},""values"":""–"",""categories"":[],""typeId"":0,""firstField"":{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0},""splitter"":true,""idAndType"":""-1"",""ident"":""-1"",""myObject"":false}]}";
            List<Application> result = new List<Application>();
            result = parser.ParseJson(action);
            Assert.AreEqual("Software Engineering", result[0].CourseName);
        }

        [TestMethod]
        public void TestParserCourseCodeSearch()
        {
            string action = @"{""count"":2, ""ids"":[189540,-1], ""records"":[{""fields"":[{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},{""values"":[""DVGC22""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":48},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":45},{""values"":[""44""],""numberOfValues"":1,""valuesAsInteger"":[44],""valuesAsBoolean"":[true],""extId"":"""",""id"":51},{""values"":[""Software Engineering""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":44}],""id"":189540,""type"":{""extId"":"""",""id"":199},""values"":""22756, DVGC22, Software Engineering, 44, Software Engineering"",""categories"":[],""typeId"":199,""firstField"":{""values"":[""22756""],""numberOfValues"":1,""valuesAsInteger"":[22756],""valuesAsBoolean"":[true],""extId"":"""",""id"":8},""splitter"":false,""idAndType"":""189540.199"",""ident"":""189540.199"",""myObject"":false},{""fields"":[{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0}],""id"":-1,""type"":{""extId"":"""",""id"":0},""values"":""–"",""categories"":[],""typeId"":0,""firstField"":{""values"":[""Separator""],""numberOfValues"":1,""valuesAsInteger"":[0],""valuesAsBoolean"":[false],""extId"":"""",""id"":0},""splitter"":true,""idAndType"":""-1"",""ident"":""-1"",""myObject"":false}]}";
            List<Application> result = new List<Application>();
            result = parser.ParseJson(action);
            Assert.AreEqual(22756, result[0].Code);
            Assert.AreEqual("DVGC22", result[0].CourseCode);
            Assert.AreEqual("Software Engineering", result[0].CourseName);
        }
    }
}
