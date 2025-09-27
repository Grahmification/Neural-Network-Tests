Neural network tests demonstrates practical examples of neural network implementations utilizing C# and winforms.

### Projects

1. [Neural Network Tests 1](Source/Neural%20Network%20Test%201) - A test implementation of basic network (no real functionality)
1. [Neural Network Tests 2](Source/Neural%20Network%20Test%202) - Utilizes a neural network to duplicate effects added to images.

### Getting started

<img src="./Example Images/Image Processing GUI Animation.gif" align="right"
     alt="GUI Screenshot" width="300" height="500">

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

### Examples


A pair of training images is shown below. The un-edited image is on the left. The image on the right has a color grading profile applied to it. The network is trained with these two images.

<p align="center">
  <img src="./Example Images/TrainingA.jpg" alt="Training Image A" width="250">
  <img src="./Example Images/TrainingB.jpg" alt="Training Image B" width="250">
</p>

The image below on the left is processed with the network trained using the two images above. The output image is shown on the right.

<p align="center">
  <img src="./Example Images/Input.jpg" alt="Input Image" width="250">
  <img src="./Example Images/Output.jpg" alt="Output Image" width="250">
</p>

Here is an example of the neural network learning a threshold effect. The training images are below. The right one has been thresholded to make a black and white image.

<p align="center">
  <img src="./Example Images/TrainingA2.jpg" alt="Training Image A" width="250">
  <img src="./Example Images/TrainingB2.jpg" alt="Training Image B" width="250">
</p>

The input and output images are below. The network does an extremely effective job of learning the threshold effect.

<p align="center">
  <img src="./Example Images/Input2.jpg" alt="Input Image" width="250">
  <img src="./Example Images/Output 2.jpg" alt="Output Image" width="250">
</p>


### How it works

- Todo

### License

![GitHub](https://img.shields.io/github/license/Grahmification/Neural-Network-Tests) Neural Network Tests is available for free under the MIT license.

Packages:
- This project licenses [Oxyplot](https://oxyplot.github.io/) under the MIT License. Copyright (c) 2014 OxyPlot contributors.


### Literature

Below are some interesting papers on neural networks and related topics.

**Image Analogies:** A far more complicated neural network processing algorithm for image processing.
- [Website](https://mrl.cs.nyu.edu/projects/image-analogies/)
- [Source Code](https://mrl.cs.nyu.edu/projects/image-analogies/lf/lf.tar.gz)
- [Papers](https://mrl.cs.nyu.edu/publications/image-analogies/)
- [Direct Paper Link](https://mrl.cs.nyu.edu/publications/image-analogies/analogies-72dpi.pdf)

**Deep Photo Style Transfer:**
- [Website](https://www.cs.cornell.edu/~fujun/files/style-cvpr17/style-cvpr17.html)
- [Source Code](https://github.com/luanfujun/deep-photo-styletransfer)
- [Direct Paper Link](https://arxiv.org/pdf/1703.07511.pdf)


### Changelog

V1.1.0 - 2025-09-27
- Update projects to dotnet 8.0
- Minor cleanup