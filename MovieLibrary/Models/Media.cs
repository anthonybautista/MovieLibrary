namespace MovieLibrary.Models
{
    public abstract class Media
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public abstract void Display();
    }
}