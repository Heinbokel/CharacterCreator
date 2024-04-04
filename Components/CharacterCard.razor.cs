using CharacterCreator.Models;
using CharacterCreator.Services;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public partial class CharacterCard: ComponentBase {

    // Injects our new CharacterService into this class so it can be used.
    [Inject]
    private CharacterService _characterService {get; set;}

    [Parameter]
    public Character Character {get; set;}

    /// <summary>
    /// Deletes a character.
    /// </summary>
    private void DeleteCharacter()
    {
        // All we do now is call the DeleteCharacter method in our CharacterService.
        // That method will take care of removing the character from its list of characters
        // and the CharacterService will then notify all observers that the character list has changed.
        this._characterService.DeleteCharacter(this.Character);
    }
}