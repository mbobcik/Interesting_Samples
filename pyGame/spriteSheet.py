import pygame
import constants


class SpriteSheet(object):
    """docstring for SpriteSheet"""

    def __init__(self, filename, width, height, transparentColor, padding):
        self.filename = filename
        self.width = width
        self.height = height
        self.transparentColor = transparentColor
        self.padding = padding
        self.ssMainImage = pygame.image.load(self.filename).convert()
        self.subSpriteSheet = None

    def getImageByCoordinates(self, x, y):
        image = pygame.Surface([self.width, self.height]).convert()
        image.blit(self.ssMainImage, (0, 0), (x, y, self.width, self.height))
        """image.set_colorkey(self.transparentColor)"""

        return image

    def getImageByNumber(self, column, row):
        x = column * self.width + column * self.padding
        y = row * self.height + row * self.padding
        return self.getImageByCoordinates(x, y)

    def MarkSubSpriteSheet(self, x, y, width, height, padding):
        pass
