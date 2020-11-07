namespace Entities.Models
{
    public class OrderInput
    {
        public string Label { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Parameter { get; set; }
        public bool IsPredefinedOnMap { get; set; }
        public bool IsSelectedTileOnMap { get; set; }
        public bool IsSelectableOnMap { get; set; }
        public string MapDescription { get; set; }
    }
}
