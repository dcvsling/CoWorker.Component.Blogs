namespace CoWorker.Helper.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// String With Func<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StringValue<T> : IEqualityComparer<StringValue<T>>
    {
        public static StringValue<T> Create(string text, Func<T> constructor) => new StringValue<T>(text, constructor);

        private string text;

        private Lazy<T> value;

        private StringValue(string text, Func<T> constructor)
        {
            this.text = text;
            this.value = new Lazy<T>(constructor);
        }

        public string Text => text;

        public T Value => value.Value;

        public int GetHashCode(StringValue<T> obj)
            => obj.GetHashCode();

        public override int GetHashCode()
            => GetHashCode(this);

        public override bool Equals(object obj)
            => Equals(this, obj as StringValue<T>);

        public bool Equals(StringValue<T> x, StringValue<T> y)
            => x.Equals(y);

        public static implicit operator string(StringValue<T> strval) => strval.Text;
        public static implicit operator T(StringValue<T> strval) => strval.Value;
    }
}