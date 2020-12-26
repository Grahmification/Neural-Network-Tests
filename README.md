Neural network tests demonstrates practical examples of neural network implementations utilizing C# and winforms.

![License](https://img.shields.io/badge/license-MIT-red.svg)

#### Projects

1. Neural Network Tests 1 - A test implementation of basic network (no real functionality)
1. Neural Network Tests 2 - Utilizes a neural network to duplicate effects added to images.

#### Getting started

1. Compile the code in Visual Studio.
1. Run Neural Network Tests 2.exe 
1. Select a working folder that contains your training and input images.
1. Specify your training images.
   * A training image pair consists of an unedited and edited photo.
   * Both images must have identical size & resolution.
   * It is recommended not to use full size images for training. Processing after the network is trained is much faster.
1. Specify a learning rate for the network and click train.
   * A loading bar will display progress. The plot will show RGB errors for each pixel as the network trains. If a reasonable learning rate is selected, these should generally decrease as the network trains.
1. Specify a new unedited picture to process with the trained network. Click process.
   * The network will attempt to apply the same effect to this image that was observed in the training images.
   * Re-processing the unedited image from the training dataset provides a good indication of how successful training was. The processed image should be very similar to the edited image from the training set.
   
Some sample image test datasets have been added to the repository. Give these a try. Currently only the network learning rate can be changed from the GUI; the primary structure of the network is hard-coded. Eventually support will be added to modify advanced network parameters from the GUI.

#### How it works

- Todo
  
