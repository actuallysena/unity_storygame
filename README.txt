Choose Your Own Ending - Story Game in UNITY
Description
This is an interactive, choice-based romantic story game built with Unity. Players can navigate through different branching storylines, make decisions, and shape the ending. The game features dynamically loaded assets, a scrollable history of choices, and the ability to resume from saved points.

Features
Choice-Based Gameplay: Players choose their path and create their own unique story.
JSON-Driven Storylines: Story data is loaded from a story.json file in the StreamingAssets folder.
Scrollable History: Review past decisions in a scrollable text window.
Dynamic UI Elements: Buttons and text fields are updated based on story progression.
Saving and Loading: Players can return to the game and resume from their last decision.
Requirements
Unity 2021 or higher.
TextMeshPro package installed (for enhanced text UI).
Setup
Clone or download the project from the repository.
Place your story JSON file in the Assets/StreamingAssets folder. The JSON file should define the story nodes and branching paths.
Add any images used in the story to the Assets/Resources/Images/ folder. The imagePath in the JSON must match the name of the image files.
Build and run the game. In the editor, use Assets > Build and Run to test.
JSON File Format
The story.json file should follow this structure:

json

{
  "storyNodes": [
    {
      "id": 0,
      "text": "This is the start of your journey.",
      "options": [
        {
          "text": "Take the left path",
          "sonrakiNodeId": 1
        }
      ]
    }
  ]
}
Usage
Play: Start the game and make choices to navigate through the story.
History: Review past decisions by clicking the "History" button.
Return to Game: After viewing history, resume from the last saved point.
Restart: Click the "Restart" button to play the story from the beginning.
Troubleshooting
If assets do not load in the built version, ensure they are in the correct Resources or StreamingAssets folders and their paths are correctly referenced in the JSON.