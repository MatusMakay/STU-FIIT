

from exc.error import Error


class ComunicationExceed(Error):
    pass
    """
    Occurs when one side dont answer on Keep Alive flag 
    or when comunication isn't initialize properly
    """