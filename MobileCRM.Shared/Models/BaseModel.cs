﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using MobileCRM.Shared.Attributes;
using Newtonsoft.Json;


namespace MobileCRM.Shared.Models
{
    public class BaseModel
    {
      public BaseModel()
      {
        Company = string.Empty;
        Street = Unit = City = PostalCode = State = Country = string.Empty;
        Phone = Title = Email = Website = Phone = Mobile = Fax = string.Empty;
        Twitter = LinkedIn = Facebook = Skype = string.Empty;
      }
      [JsonProperty(PropertyName = "id")]
      public string Id { get; set; }

      [Microsoft.WindowsAzure.MobileServices.Version]
      public string Version { get; set; }

      [JsonProperty(PropertyName = "company")]
      public string Company { get; set; }

      [JsonProperty(PropertyName = "title")]
      public string Title { get; set; }

      [JsonProperty(PropertyName = "email")]
      public string Email { get; set; }

      [JsonProperty(PropertyName = "website")]
      public string Website { get; set; }

      [JsonProperty(PropertyName = "phone")]
      public string Phone { get; set; }

      [JsonProperty(PropertyName = "mobile")]
      public string Mobile { get; set; }

      [JsonProperty(PropertyName = "fax")]
      public string Fax { get; set; }

      [JsonProperty(PropertyName = "twitter")]
      public string Twitter { get; set; }

      [JsonProperty(PropertyName = "linkedin")]
      public string LinkedIn { get; set; }

      [JsonProperty(PropertyName = "facebook")]
      public string Facebook { get; set; }

      [JsonProperty(PropertyName = "skype")]
      public string Skype { get; set; }

      [JsonProperty(PropertyName = "street")]
      public string Street { get; set; }

      [JsonProperty(PropertyName = "unit")]
      public string Unit { get; set; }

      [JsonProperty(PropertyName = "city")]
      public string City { get; set; }

      [Display("Postal Code")]
      [JsonProperty(PropertyName = "postal_code")]
      public string PostalCode { get; set; }


      [JsonProperty(PropertyName = "state")]
      public string State { get; set; }

      [JsonProperty(PropertyName = "country")]
      public string Country { get; set; }

      [JsonProperty(PropertyName = "latitude")]
      public double Latitude { get; set; }

      [JsonProperty(PropertyName = "longitude")]
      public double Longitude { get; set; }

      [JsonIgnore]
      public string AddressString
      {
        get { return string.Format("{0}{1} {2} {3} {4}", Street, !string.IsNullOrWhiteSpace(Unit) ? Unit + "," : string.Empty, !string.IsNullOrWhiteSpace(City) ? City + "," : string.Empty, State, PostalCode); }
      }

      [JsonIgnore]
      public string DisplayName
      {
        get { return this.ToString(); }
      }
    }
}
