using CharacterCreator.Models;
using CharacterCreator.Services;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public partial class CharacterList: ComponentBase {

    // Injects our new CharacterService into this class so it can be used.
    [Inject]
    private CharacterService _characterService {get; set;}

    // Same field that holds our characters, but we default it to an empty collection
    // because we will now retrieve characters from the CharacterService.
    private List<Character> Characters {get; set;} = [];

    /// <summary>
    /// Method that automatically runs when this component is intialized.
    /// Retrieves the characters from the character service and assigns those characters to our local characters list.
    /// </summary>
    protected override void OnInitialized()
    {
        // This is where we are "Subscribing" or "Observing" our Character Subject in the Character Service.
        // EVERY time the character service's BehaviorSubject receives a new value, the logic in this subscription
        // will be run again. So each time the character list changes, we will receive those changes here.
        this._characterService.GetCharacters().Subscribe(newCharacters =>
        {
            // Set our component's characters to the new list of characters that we observed.
            this.Characters = newCharacters;

            // This will update the UI by letting it know that something has changed in our values
            // and that it needs to rerender.
            StateHasChanged();
        });
    }

}