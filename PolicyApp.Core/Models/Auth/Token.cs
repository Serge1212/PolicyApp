using System;

namespace PolicyApp.Core.Models.Auth {
  public class Token {
    public string Value { get; set; }
    public DateTime ExpiryDate { get; set; }
  }
}
