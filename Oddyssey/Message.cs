using System;
using System.Xml.Serialization;

namespace Oddyssey
{
    public class Message
    {
        public string Opcode;
        public string Data;
        
        
        public void SetOpcode(String opcode)
        {
            Opcode = opcode;
        }

        public String GetOpcode()
        {
            return Opcode;
        }

        public void SetData(String data)
        {
            Data = data;
        }

        public String GetData()
        {
            return Data;
        }
    }
}