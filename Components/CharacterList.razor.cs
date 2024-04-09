using System.Web;
using CharacterCreator.Models;
using CharacterCreator.Services;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Components;

public partial class CharacterList: ComponentBase {
    // Injects the NavigationManager to be used in this class.
    [Inject]
    public NavigationManager NavigationManager {get; set;}
    
    // Injects our new CharacterService into this class so it can be used.
    [Inject]
    private CharacterService _characterService {get; set;}

    // Same field that holds our characters, but we default it to an empty collection
    // because we will now retrieve characters from the CharacterService.
    private List<Character> Characters {get; set;} = [];

    private bool DisplayFeedbackMessage {get; set;}

    // Variable used to display a feedback message to the user.
    private string FeedbackMessage { get; set; }

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

        this.DetermineFeedbackMessages();
    }

    /// <summary>
    /// Determines the feedback messages to display.
    /// </summary>
    private void DetermineFeedbackMessages()
    {
        // Get the current URL as a Uri so we can parse it easier.
        var uri = new Uri(NavigationManager.Uri);

        // Get the query parameters with our built in HttpUtility method.
        var queryParams = HttpUtility.ParseQueryString(uri.Query);

        // Get the value of the "deletedCharacter" parameter
        var deletedCharacterName = queryParams["deletedCharacter"];

        // Check if deletedCharacter param even exists. If so, update our feedback message.
        if (!string.IsNullOrEmpty(deletedCharacterName))
        {
            // Update feedback state
            this.DisplayFeedbackMessage = true;
            this.FeedbackMessage = $"{deletedCharacterName} was successfully deleted.";
        }
    }
}