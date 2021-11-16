#import pygame
from constants import Constants
from vector2 import Vector2

class GameObject(object):
    """
        Class representing object in game world.
        Object can contain sprite to render or
        sub objects in tree-like structure.

        Attributes:
            Position:
    """

    def render(self, display):
        """

        """
        if self.sprite is not None:
            display.blit(self.sprite, (self.getXWorldPos(), self.getYWorldPos()))
        for subObject in self.subObjects:
            subObject.render(display)

    def __init__(self, parent=None):
        self.Position = Vector2(0, 0)
        self.width = 0
        self.height = 0
        self.Direction = Vector2(0, 0)
        self.sprite = None
        self.type = Constants.ObjectType.Null
        self.subObjects = list()
        self.parent = parent

    def addSubObject(self, subObject):
        subObject.parent = self
        self.subObjects.append(subObject)

    def setSprite(self, sprite, width, height):
        self.sprite = sprite
        self.width = width
        self.height = height

    def getXWorldPos(self):
        if self.parent is None:
            return self.Position.x
        return self.parent.getXWorldPos() + self.Position.x

    def getYWorldPos(self):
        if self.parent is None:
            return self.Position.y
        return self.parent.getYWorldPos() + self.Position.y

    def setDirection(self, x, y):
        self.Direction.set(x, y)

    def setDirectionVec(self, vector):
        self.Direction = vector

    def applyDirection(self):
        self.Position += self.Direction

    def setX(self, x):
        self.Position.x = x

    def setY(self, y):
        self.Position.y = y

    def setPos(self, x, y):
        self.Position.set(x, y)

    def isTouchingEdge(self):
        result = False

        if self.getXWorldPos() <= 0:
            result = True
        if self.getXWorldPos() + self.width >= Constants.MAIN_WINDOW_WIDTH:
            result = True

        if self.getYWorldPos() <= 0:
            result = True
        if self.getYWorldPos() + self.height >= Constants.MAIN_WINDOW_HEIGHT:
            result = True

        for obj in self.subObjects:
            result = result or obj.isTouchingEdge()

        return result
