﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyLaboratory.WebSite.Models.ViewModels.Notice
{
    public class FixedExpenditureInputViewModel
    {
        [Required(ErrorMessage = "Please enter Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter MainClass")]
        [Display(Name = "MainClass")]
        public string MainClass { get; set; }
        [Required(ErrorMessage = "Please enter SubClass")]
        [Display(Name = "SubClass")]
        public string SubClass { get; set; }
        [Required(ErrorMessage = "Please enter Contents")]
        [Display(Name = "Contents")]
        public string Contents { get; set; }
        [Required(ErrorMessage = "Please enter Amount")]
        [Display(Name = "Amount")]
        public long Amount { get; set; }
        [Required(ErrorMessage = "Please enter DepositMonth")]
        [Display(Name = "DepositMonth")]
        public byte DepositMonth { get; set; }
        [Required(ErrorMessage = "Please enter DepositDay")]
        [Display(Name = "DepositDay")]
        public byte DepositDay { get; set; }
        [Required(ErrorMessage = "Please enter MaturityDate")]
        [Display(Name = "MaturityDate")]
        public string MaturityDate { get; set; }
        [Display(Name = "Note")]
        public string Note { get; set; }
        [Required(ErrorMessage = "Please enter PaymentMethod")]
        [Display(Name = "PaymentMethod")]
        public string PaymentMethod { get; set; }
        [Required(ErrorMessage = "Please enter MyDepositAsset")]
        [Display(Name = "MyDepositAsset")]
        public string MyDepositAsset { get; set; }
    }
}