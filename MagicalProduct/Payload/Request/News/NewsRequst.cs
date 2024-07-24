namespace MagicalProduct.API.Payload.Request
{
    public class CreateNewsRequest
    {
        [FromForm(Name = "title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [FromForm(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        [FromForm(Name = "content")]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }

    public class UpdateNewsRequest
    {
        [FromForm(Name = "id")]
        [Required(ErrorMessage = "News Id is required")]
        public int Id { get; set; }

        [FromForm(Name = "title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [FromForm(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        [FromForm(Name = "content")]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }
}
