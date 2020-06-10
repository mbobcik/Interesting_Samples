import pygame
from pygame.locals import *
from constants import *
from spriteSheet import *


class Game:

    def __init__(self):
        self._running = True
        self._display_surf = None

    def on_init(self):
        pygame.init()
        self._display_surf = pygame.display.set_mode(
            Constants.MAIN_WINDOW_SIZE, pygame.HWSURFACE | pygame.DOUBLEBUF)
        self.platformsSS = SpriteSheet("content/tiles_spritesheet_platformer.png", 70, 70, Constants.Color.WHITE, 2)
        self.image = self.platformsSS.getImageByCoordinates(0, 0)
        self.image2 = self.platformsSS.getImageByNumber(1, 0)
        self._running = True

    def on_event(self, event):
        if event.type == pygame.QUIT:
            self._running = False

    def on_loop(self):
        pass

    def on_render(self):
        self._display_surf.blit(self.image2, (380, 380))
        self._display_surf.blit(self.image, (300, 300))
        pygame.display.flip()

    def on_cleanup(self):
        pygame.quit()

    def on_execute(self):
        if self.on_init() is False:
            self._running = False

        while self._running:
            for event in pygame.event.get():
                self.on_event(event)

            self.on_loop()
            self.on_render()

        self.on_cleanup()


if __name__ == "__main__":
    theGame = Game()
    theGame.on_execute()
