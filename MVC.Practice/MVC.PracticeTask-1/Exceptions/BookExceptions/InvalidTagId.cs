﻿namespace MVC.PracticeTask_1.Exceptions.BookExceptions
{
    public class InvalidTagId : Exception
    {
        public string PropertyName { get; set; }
        public InvalidTagId() { }
        public InvalidTagId(string propertyName, string? message) : base(message)
        {

        }
    }
}
