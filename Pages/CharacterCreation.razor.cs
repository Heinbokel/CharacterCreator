using CharacterCreator.Models;
using CharacterCreator.Services;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Pages;

public partial class CharacterCreation: ComponentBase {
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    // Inject our Character Service as we have seen elsewhere.
    [Inject]
    private CharacterService _characterService {get; set;}

    // Holds our Character that our form is bound to.
    private Character Character {get; set;} = new Character();

    // Submits the form, creating the character and resetting our local character.
    // The Character Service will take care of adding this character to its list 
    // and notifying all observers that the list of characters has changed.
    private void SubmitForm() {
        this._characterService.AddCharacter(this.Character);
        this.NavigationManager.NavigateTo($"/character-details/{this.Character.Id}?createdCharacter={this.Character.Name}");
        this.Character = new Character();
    }

}