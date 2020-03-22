using System;

namespace MedClinic.Model
{
    public class PatientModel:UserModel
    {
        public bool IsMan { get; set; }
        public bool IsWoman { get; set; }
        public string PassData { get; set; }
        public string MedData { get; set; }   
        public string Photo { get; set; }
        public byte[] PhotoBytes { get; set; }
        public ModelState ModelState { get; set; }
    }
}
