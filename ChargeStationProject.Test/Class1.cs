using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject.Test
{

    class FakeUserValidation : IRfidReader
    {
       
        {
            private int _correctRfid = "1234";

            private bool _validRfid = false;

            public bool GetWasIDCorrectForTest()
            {
                return _validRfid;
            }

            public void SetCorrectIDForTest(int correctID)
            {
            _correctRfid = correctRfid;
            }

            public bool ValidateEntryRequest(int id)
            {
                Console.WriteLine("FakeUserValidation::ValidateEntryRequest called");

                if (_correctRfid == Rfid)
                {
                    _validRfid = true;
                }

                return _validRfid;
            }
        }
    }
}
    