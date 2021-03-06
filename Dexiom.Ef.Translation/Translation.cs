﻿namespace Dexiom.Ef.Translation
{
    public abstract class Translation<T> where T : Translation<T>, new()
    {
        public int Id { get; set; }

        public string CultureName { get; set; }
    }
}