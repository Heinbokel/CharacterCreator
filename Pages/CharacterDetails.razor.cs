using CharacterCreator.Models;
using CharacterCreator.Services;
using Microsoft.AspNetCore.Components;

namespace CharacterCreator.Pages;

public partial class CharacterDetails: ComponentBase {

    // Injects the NavigationManager so we can utilize it in this class.
    [Inject]
    public NavigationManager NavigationManager {get; set;}

    // This is the ID of the character we want to retrieve, which is passed in via the URL.
    [Parameter]
    public int Id {get; set;}

    // Here we inject our CharacterService into this class.
    // This is functionally the same as injecting it using class Constructors.
    [Inject]
    public CharacterService CharacterService {get; set;}

    // This is the Character that will come from the CharacterService, stored in this class.
    private Character? Character;

    // These are variables used to store the states of loading/errors.
    private bool IsLoading = true;
    private bool ErrorOccurred = false;
    private string ErrorMessage = "";

    /// <summary>
    /// Logic to run on the OnInitialized lifecycle hook.
    /// This runs after this component has rendered to the screen.
    /// </summary>
    protected override void OnInitialized()
    {
        RetrieveCharacter();
    }

    /// <summary>
    /// Retrieves and sets the Character for this component.
    /// </summary>
    private void RetrieveCharacter()
    {
        // Reset our loading/error indicators.
        this.IsLoading = true;
        this.ErrorMessage = "";
        this.ErrorOccurred = false;

        // Subscribe (observe) to the Observable emitted by GetCharacterById.
        this.CharacterService.GetCharacterById(Id).Subscribe(
                    character =>
                    {
                        // Once a character is emitted from the observable, check to see if it's null.
                        // If it's not null, all is well, set our local Character to the Character that came back.
                        // Otherwise, let the user know an error occurred by setting our error variables.
                        if (character != null)
                        {
                            this.Character = character;
                        }
                        else
                        {
                            this.ErrorOccurred = true;
                            this.ErrorMessage = $"Character with ID {Id} was not found.";
                        }
                        this.IsLoading = false;
                    }
                );
    }

    /// <summary>
    /// Deletes the character associated with this page and navigates the user back to home.
    /// </summary>
    private void DeleteCharacter() {
        // Delete the character.
        this.CharacterService.DeleteCharacter(this.Character);

        // Navigate to the Home Page (which is defined with a path of '/', so just the base URL of the website)
        // including a URL parameter.
        this.NavigationManager.NavigateTo($"?deletedCharacter={this.Character.Name}");
    }
}