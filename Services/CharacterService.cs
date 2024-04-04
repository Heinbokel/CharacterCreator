namespace CharacterCreator.Services;

using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CharacterCreator.Models;

public class CharacterService
{
    // This holds our list of characters, + our intial characters.
    private List<Character> Characters = new List<Character>
        {
            new Character
            {
                Name = "Rytlock Brimstone",
                Class = "Revenant",
                Race = "Charr",
                DateOfBirth = new DateOnly(1319, 4, 10),
                Biography = "A fierce Charr warrior who became the first Revenant."
            },
            new Character
            {
                Name = "Caithe",
                Class = "Thief",
                Race = "Sylvari",
                DateOfBirth = new DateOnly(1302, 2, 18),
                Biography = "A mysterious Sylvari who is one of the firstborn."
            },
            new Character
            {
                Name = "Logan Thackeray",
                Class = "Guardian",
                Race = "Human",
                DateOfBirth = new DateOnly(1303, 8, 15),
                Biography = "A noble Human guardian and leader of the Seraph."
            },
            new Character
            {
                Name = "Zojja",
                Class = "Engineer",
                Race = "Asura",
                DateOfBirth = new DateOnly(1309, 11, 1),
                Biography = "A brilliant Asura inventor and member of Destiny's Edge."
            },
            new Character
            {
                Name = "Rox",
                Class = "Ranger",
                Race = "Charr",
                DateOfBirth = new DateOnly(1325, 6, 22),
                Biography = "A skilled Charr ranger and member of the Stone Warband."
            },
            new Character
            {
                Name = "Braham Eirsson",
                Class = "Warrior",
                Race = "Norn",
                DateOfBirth = new DateOnly(1327, 9, 30),
                Biography = "A young Norn warrior and the son of the legendary Eir Stegalkin."
            },
            new Character
            {
                Name = "Marjory Delaqua",
                Class = "Necromancer",
                Race = "Human",
                DateOfBirth = new DateOnly(1314, 12, 5),
                Biography = "A skilled Human necromancer and member of the Durmand Priory."
            },
            new Character
            {
                Name = "Kasmeer Meade",
                Class = "Mesmer",
                Race = "Human",
                DateOfBirth = new DateOnly(1316, 7, 28),
                Biography = "A talented Human mesmer and member of the Ministry Guard."
            },
            new Character
            {
                Name = "Taimi",
                Class = "Elementalist",
                Race = "Asura",
                DateOfBirth = new DateOnly(1328, 3, 12),
                Biography = "A genius Asura elementalist and assistant to the Pact Commander."
            },
            new Character
            {
                Name = "Frank Reynolds",
                Class = "Revenant",
                Race = "Human",
                DateOfBirth = new DateOnly(1319, 4, 10),
                Biography = "A fierce Human warrior."
            }
        };

    // This is a Subject that will be Observed. In particular, the BehaviorSubject will retain 
    // the last value that was provided to it so that all new subscribers can receive the latest value.
    private readonly BehaviorSubject<List<Character>> CharactersSubject = new BehaviorSubject<List<Character>>([]);

    /// <summary>
    /// Default Constructor for CharacterService. 
    /// Sets the intial value of the CharacterSubject.
    /// </summary>
    public CharacterService() {
        this.CharactersSubject.OnNext(this.Characters);
    }

    /// <summary>
    /// Returns our BehaviorSubject as an Observable that we can Observe.
    /// </summary>
    /// <returns>The observable to return.</returns>
    public IObservable<List<Character>> GetCharacters() {
        return this.CharactersSubject.AsObservable();
    }

    /// <summary>
    /// Adds a character to our list of characters and notifies all observers.
    /// </summary>
    /// <param name="character">The character to add to our list.</param>
    public void AddCharacter(Character character)
    {
        Characters.Add(character);
        CharactersSubject.OnNext(Characters);
    }

    /// <summary>
    /// Removes a character from our list of characters and notifies all observers.
    /// </summary>
    /// <param name="characterToDelete">The character to delete.</param>
    public void DeleteCharacter(Character characterToDelete)
    {
        Characters.Remove(characterToDelete);
        CharactersSubject.OnNext(Characters);
    }
}