class Knight:
    
    move_x = [2, 1, -1, -2, -2, -1, 1, 2]
    move_y = [1, 2, 2, 1, -1, -2, -2, -1]
    steps = 0


    def get_move(self, num):
        return self.move_x[num], self.move_y[num]
    #pocita ako moc hlboko sa program pri rekurzii ponori
    def steps_up(self):
        self.steps = self.steps + 1
    
    def steps_down(self):
        self.steps -= 1

    def get_steps(self):
        return self.steps
