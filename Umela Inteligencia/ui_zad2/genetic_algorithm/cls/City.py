class City:
   
    def __init__(self, x, y, name):
        self.name = name
        self.set_cordinates(x, y)
        
    def __str__(self):
        return f"{self.name}"

    def set_cordinates(self, x, y):
        self.cordinates = (x, y)

    def get_cordinates(self):
        return self.cordinates[0], self.cordinates[1]

    def get_cordinates_euclid(self):
        return (self.cordinates[0], self.cordinates[1])
    
    def get_name(self):
        return self.name
