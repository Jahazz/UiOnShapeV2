
# UiOnShape

Lightweight package for creating UI on meshes. This POC bases on saving ui canvas camera output in RenderTexture that is later assigned as mesh material texture. Input cursor position is then converted using raycasting on texture position and InputProcessor(thus input system is needed).


## Usage/Examples

Setup input system(event system, inputSystemUIInputModule, PlayerInput). Add ShapeRepositionProcessor.cs to your input actions UI "point". Add UiOnShapeCaster.cs to scene and setup fields. Remember that object that you want UI on needs to have properly setup UV(full UV rect should be populated for default behaviour). In project there are 2 demo scenes but if you have any questions feel free to msg me.


## License
[MIT](https://choosealicense.com/licenses/mit/)

