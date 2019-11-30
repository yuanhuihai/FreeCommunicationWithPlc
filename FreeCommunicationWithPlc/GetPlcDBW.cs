using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;

namespace FreeCommunicationWithPlc
{
    class GetPlcDBW
    {
        S7Client Client = new S7Client();
        private byte[] Buffer = new byte[65536];

        public int getPlcDbwValue(string plcIp,int Rack,int Slot,int DbNum,int DbwNum)
        {
            Client.ConnectTo(plcIp, Rack, Slot);        
            Client.DBRead(DbNum, DbwNum, 2, Buffer);//读取DbwNum所对应的字的值
            Client.Disconnect();
            return  S7.GetIntAt(Buffer, 0);           
        }

        public void resetPlcDbwValue(string plcIp, int Rack, int Slot, int DbNum, int DbwNum)
        {          
            Client.ConnectTo(plcIp, Rack, Slot);
            byte[] buffer = new byte[65536];
            Client.DBWrite(DbNum,DbwNum,2, buffer);//将DbwNum对应的字赋值为0
            Client.Disconnect();
        }
    }
}
