using System;
using System.Collections.Generic;
using System.Text;

namespace EduxSoft.Base.Models
{
    public class ModalFooter
    {
        public string SubmitButtonText { get; set; } = "Terminer";
        public string CancelButtonText { get; set; } = "Annuler";
        public string SubmitButtonID { get; set; } = "btn-submit";
        public string CancelButtonID { get; set; } = "btn-cancel";
        public string OKButtonID { get; set; } = "btn-submit";
        public string OKButtonText { get; set; } = "OK";
        public bool OnlyOKButton { get; set; }
    }
}
