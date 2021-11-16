class Constants:
    """docstring for Constants"""
    MAIN_WINDOW_SIZE = MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT = 800, 600

    class Color:
        BLACK = (0, 0, 0)
        WHITE = (255, 255, 255)
        BLUE = (0, 0, 255)

    class ObjectType:
        Null = 0
        Pad = 1
        Ball = 2
        Player = 3

    class Paths:
        Spritesheet = "pyGame/content/tiles_spritesheet_platformer.png"
