namespace Framework.Models;
public class MirrorCard : Card

{
    public override string Name { get; set; } = "Mirror";
    public override int Cost { get; set; } = 1;
    public override string Rarity { get; set; } = "";
    public override string Type {get; set;} = "Spell";
    public override int Arena { get; set; } = 12;

}

