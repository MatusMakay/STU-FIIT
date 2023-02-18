from exc.error import Error 

class SwitchRoles(Error):
    """
    Occurs when user want to switch roles of receiver and sender
    """
    def __init__(self, value):
        self.value = value
   
    # __str__ is to print() the value
    def __str__(self):
        return f"{self.value}"

    def get_port(self):
        self.value