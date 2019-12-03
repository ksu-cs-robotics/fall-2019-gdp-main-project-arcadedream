To get NPC/player interaction to work properly:
-player object tagged as "Player"
-npc object tagged as "NPC"
-create sphere collider around npc (as big as you want the interaction radius), set as trigger

PlayerController.cs script must be attached to the player object. This script uses a 
variable "DIALOGUECANVAS". Everything for the NPC dialogue (buttons, 
text, etc) should be housed under one canvas and dragged to this variable.

NpcInteraction.cs script must be attached to the npc objects. This script uses a 
variable "INTERACTTEXT". Create a text element under the global canvas to say
"[X] To Talk". This text element needs to be dragged to this variable.
