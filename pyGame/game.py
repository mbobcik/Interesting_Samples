import random as rand

import pygame
#from pygame.locals import *
from constants import Constants
from spriteSheet import SpriteSheet
from gameObject import GameObject
import random as rand


class Game:

    def __init__(self):
        self.topLevelObjects = list()
        self._running = True
        self._display_surf = None
        self.platformsSS = None
        self.image2 = None
        self.image = None
        self.ball = None

    def on_init(self):
        pygame.init()
        self._display_surf = pygame.display.set_mode(
            Constants.MAIN_WINDOW_SIZE, pygame.HWSURFACE | pygame.DOUBLEBUF)
        self.platformsSS = SpriteSheet(
            Constants.Paths.Spritesheet, 70, 70, Constants.Color.WHITE, 2)
        self.image2 = self.platformsSS.getImageByCoordinates(0, 0)
        self.image = self.platformsSS.getImageByNumber(0, 11)

        # TODO udělat objekt World asi ??

        x = GameObject()
        x.setPos(20, 1)
        x.setSprite(self.image, 70, 70)
        x.setDirection(0, 3)
        y = GameObject(x)
        y.setSprite(self.image, 70, 70)
        y.setY(70)
        x.addSubObject(y)

        z = GameObject()
        z.setX(Constants.MAIN_WINDOW_WIDTH - 90)
        z.setY(Constants.MAIN_WINDOW_HEIGHT - 150)
        z.setSprite(self.image, 70, 70)
        z.setDirection(0, -3)
        m = GameObject(z)
        m.setSprite(self.image, 70, 70)
        m.setY(70)
        z.addSubObject(m)

        ball = GameObject()
        ball.setX(Constants.MAIN_WINDOW_WIDTH / 2)
        ball.setY(Constants.MAIN_WINDOW_HEIGHT / 2)
        ball.type = Constants.ObjectType.Ball
        ball.setDirection(3, 0)
        image = self.platformsSS.getImageByNumber(6, 7)
        ball.setSprite(image, 70, 70)
        self.ball = ball

        self.topLevelObjects.append(x)
        self.topLevelObjects.append(z)
        self.topLevelObjects.append(ball)
        self._running = True

        pygame.time.wait(500)

    def on_event(self, event):
        if event.type == pygame.QUIT:
            self._running = False

        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_SPACE:
                newAngle = rand.randint(0, 360)
                print("new angle = " + str(newAngle))
                self.ball.Direction.setAngle(newAngle)
                print("angle = " + str(self.ball.Direction.getAngle()))

    def on_loop(self):
        for obj in self.topLevelObjects:
            if obj.isTouchingEdge():
                obj.Direction = -obj.Direction

            obj.applyDirection()

        pygame.time.delay(30)
        pygame.display.update()

    def on_render(self):
        #reset display
        self._display_surf.fill(Constants.Color.BLACK) 

        #render all objects
        for obj in self.topLevelObjects:
            obj.render(self._display_surf)

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
