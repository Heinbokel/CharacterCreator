namespace CharacterCreator.Models;

public class Character {
    public int Id { get; set;}
    
    public string Name {get; set;}

    public string Class {get; set;}

    public string Race {get; set;}

    public DateOnly DateOfBirth {get; set;}

    public string Biography {get; set;}

}