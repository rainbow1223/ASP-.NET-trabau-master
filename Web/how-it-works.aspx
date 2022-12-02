<%@ Page Title="How it Works? - Trabau" Language="C#" MasterPageFile="~/index.master" AutoEventWireup="true" CodeFile="how-it-works.aspx.cs" Inherits="how_it_works" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="inner-page-banner howItwork-banner">
        <div class="container">
            <div class="row d-flex align-items-center">
                <div class="col-lg-5">
                    <div class="bannerContent">
                        <h2>How It Works</h2>
                        <p>At Trabau, we help you identify a problem and a solution is provided to help you solve that problem.  The overall process is better described in the steps below.</p>
                        <a href="#" class="cta-btn-md">Get Started</a>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="bannerGraphic">
                        <img src="../assets/uploads/banner-graphic-10.png" alt="bannerGraphic" />
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="howItWork-sec p-80">
        <div class="container">
            <div class="row">
                <div class="col-xl-3 col-lg-4 mb-3">
                    <ul class="nav nav-pills flex-column" id="myTab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true"><i class="flaticon-null"></i>Have a problem</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false"><i class="flaticon-null-1"></i>Solve a problem</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="contact-tab" data-toggle="tab" href="#contact" role="tab" aria-controls="contact" aria-selected="false"><i class="flaticon-null-2"></i>FAQs</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="steps-tab" data-toggle="tab" href="#steps" role="tab" aria-controls="steps" aria-selected="false"><i class="flaticon-notebook"></i>Step-by-step</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="cost-tab" data-toggle="tab" href="#cost" role="tab" aria-controls="cost" aria-selected="false"><i class="flaticon-safe"></i>Fee</a>
                        </li>
                    </ul>
                </div>
                <!-- /.col-md-4 -->
                <div class="col-xl-9 col-lg-8">
                    <div class="tab-content" id="myTabContent">
                        <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                            <div class="ht-block">
                                <h4>Have a problem?</h4>
                                <p>Need fast, professional services to solve a problem? Welcome to Trabau.</p>
                                <p>Find top freelancers, agencies, and contractors to help you solve that problem at ease.  Look through our specially curated list of profiles and find the perfect skillset to help you solve the problem.  </p>

                                <ul class="ht-list">
                                    <li>
                                        <h5>Start by posting a job.</h5>
                                        <p>
                                            After setting up your profile as a client, you can start posting jobs. Pick the specific categories where you need help so that the right solution providers can apply for your jobs.  Once you post a job, you can also invite freelancers, agencies, and contractors you have worked with or are impressed by, to apply for that job.  
                                        </p>
                                    </li>
                                    <li>
                                        <h5>Search our database for specific problem-solving skills.</h5>
                                        <p>Our search looks through thousands of profiles to identify the perfect fit with the right skillset. We help you to identify the specific freelancers, agencies, and contractors with specific skills that match your job requirements to help you solve your problem professionally.</p>
                                    </li>
                                    <li>
                                        <h5>Hire a freelancer.</h5>
                                        <p>Once you identify the freelancer, agency, or contractor whose skill set matches your requirements, and you are satisfied with the profile, you can then hire that user to help you solve the problem.</p>
                                    </li>
                                    <li>
                                        <h5>Bring your employees. </h5>
                                        <p>Instead of hiring a freelancer on Trabau to work on your project, you can simply create the project on this platform and bring your own people, freelancers, agencies, and contractors that you work with and simply manager them through Trabau. Our platform enables you to manage all the people who work for you remotely and in-person. </p>
                                    </li>
                                </ul>
                                <div class="howItwork-img">
                                    <img src="assets/uploads/how-to-work-tab.jpg" alt="image" />
                                </div>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                            <div class="ht-block">
                                <h4>Solve a problem</h4>
                                <p>Got the right skills, experience, and knowledge to solve problems? Find jobs as a freelancer, agency, or contractors to start working with clients.  You can look through available jobs and select those who match your skillset to solve the problem!  </p>
                                <ul class="ht-list">
                                    <li>
                                        <h5>Start by creating your profile.</h5>
                                        <p>You can start applying for jobs on specific categories after creating your profile. Make sure you include your past experience and available skills so that clients with problems can pick you to solve them.  Once you have a freelancer profile, you can apply for jobs. </p>
                                    </li>
                                    <li>
                                        <h5>Search our database for available jobs.</h5>
                                        <p>Our search functionality can help you to identify specific jobs, problems, and available tasks, with requirements that match your experience and skills.  You can search for jobs based on certain categories. </p>
                                    </li>
                                    <li>
                                        <h5>Get hired.</h5>
                                        <p>Once you identify the job(s) and requirement that fits your skill set, apply and get hired!</p>
                                    </li>
                                </ul>
                                <%-- <h4>Cost and Compensation</h4>
                                <p>Once you create a project and assign a function to a person to help you solve a problem, you will have the option to deposit funds as compensation for that person. The funds will be marked as deposit in your account and you will have option to release it to that person any time before or after the project is completed.  You can also make partial payment at any time while the project is going on. </p>
                                <p>Currently, we take 10% commission from each freelancer or people who receive money for a project.  This fee can change anytime. At any time during the project or after the project is completed, you have the option to provide bonus to a person who worked on your project to help you solve a problem.</p>
                                <br>
                                <h4>Job Posting (Employer)</h4>
                                <p>If you are posting a job to hire someone to work on a project and you plan to pay that person outside Trabau, there is a $5 fee for posting the job.</p>
                                <br />
                                <h4>Transactional Type Project</h4>
                                <p>For transactional type projects such as buying and selling an item, or sending and receiving money, there will be a $5 fee when the transaction is completed.  Refer to the fee list for more information. For transactional projects, you can deposit funds for the project and release it when the job is done or before the job ends.</p>
                                <p>For transactional projects, you don’t post the item you have for sale or you want to buy. For example, if you are buying or renting a house and you find that listing on another website and you are interested, you can create a project here to buy or rent that house. In this case, you perform all your interactions with that individual here as well as the initial money deposit. If that person already has an account here, you can simply look for his/her user name and add it to the project. If that person does not have an account yet, you can create a link of that project and email it to that person. Once the person clicks on the link that person can then register and be added to the project.</p>
                                <br />
                                <h4>Price List</h4>
                                <p>Currently, we only charge 10% commission from each freelancer or people who receive money for a project.  This price can change at any time. </p>--%>

                                <div class="howItwork-img">
                                    <img src="assets/uploads/how-to-work-tab.jpg" alt="image" />
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="contact" role="tabpanel" aria-labelledby="contact-tab">
                            <div class="ht-block">
                                <h4>FAQs</h4>
                                <p>Trabau connects freelancers, agencies, and contractors with clients who have problems and need professional solutions.  At Trabau, freelancers, agencies, and contractors who are professionals in their areas of work, are considered to be solution providers whereas clients (employers who are companies or individuals) are considered to be in need of these solutions.  </p>

                                <ul class="ht-list">
                                    <li>
                                        <h5>What type of needs (jobs) can be placed on Trabau?</h5>
                                        <p>At Trabau, we have freelancers, agencies, and contractors with the skill sets to help you solve a variety of problems that you may have. It does not matter if the problem needs remote or in-person services, our freelancers cater for that. You can also get freelancers for any category of work, at any location within the highlighted regions.  For instance, you may need a person to design a logo for your organization and that is a task that can be done remotely. You may also need a person to install a water heater for you or fix your AC which is a solution that must be done in-person.</p>
                                    </li>
                                    <li>
                                        <h5>How are the projects on Trabau managed?</h5>
                                        <p>The Trabau platform gives you full control of your project; including creating and managing every process. If you don’t want to manage your own project, you can also hire a project manager from Trabau to oversee your project for you, professionally.</p>
                                    </li>
                                    <li>
                                        <h5>How does the payment system work?</h5>
                                        <p>Payment is made by client at the beginning or end of a project and stored in the Trabau account. Payment for the freelancer, agency, or contractor, can be made at any time based on agreement with the client.  </p>
                                    </li>
                                    <li>
                                        <h5>How safe is it to hire/work in-person?</h5>
                                        <p>Trabau verifies the information provided by any client or freelancer to make sure that your security is assured while you get the best service for problem solving. </p>
                                    </li>
                                    <li>
                                        <h5>Who can work/hire on Trabau?</h5>
                                        <p>Any freelancer, agency, or contractor who has the right skills, experience, and knowledge, can be a professional problem solver on Trabau. Also, any client or organization with a valid profile and problems that need solutions can hire on Trabau. </p>
                                    </li>
                                    <li>
                                        <h5>How does the telework project manager work? </h5>
                                        <p>A project can involve multiple employees who are working remotely. The Trabau platform enables you to manage them seamlessly, just like you would if they are working in-person. When you create a project on Trabau, you can bring and manage your own employees, in which case the payment made will only be for Trabau management. There is no commission involved and you don’t have to disclose payment for the employees. </p>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="steps" role="tabpanel" aria-labelledby="steps-tab">

                            <h5>Step 1</h5>
                            <p>
                                First, a problem is identified by an employer who seeks a solution. That person or employer posts the problem and job needed to be done here, to find a solution for the problem. Examples of some solutions for problems are the following jobs: proofreading of a document, designing an e-commerce website, develop a mobile application, installation of a water heater and so on.  
                            </p>
                            <p>Let’s show the needs and the problems next to each other for easy understanding.</p>

                            <table class="table table-bordered">
                                <tr>
                                    <th class="text-center">Needs (JOB)
                                    </th>
                                    <th class="text-center">Problems
                                    </th>
                                </tr>
                                <tr>
                                    <td>Proofread of a Document</td>
                                    <td>Syntax Needs Correction</td>
                                </tr>
                                <tr>
                                    <td>Design an eCommerce Website</td>
                                    <td>Need to sell goods online</td>
                                </tr>
                                <tr>
                                    <td>Develop a Mobile Application</td>
                                    <td>Platform to provide a service to users</td>
                                </tr>
                                <tr>
                                    <td>Install a Water Heater</td>
                                    <td>Broken Water Heater</td>
                                </tr>
                            </table>
                            <br />
                            <h5>Step 2</h5>
                            <p>
                                After identifying the problem, the employer posts a job on Trabau to find a person who has the skills and knowledge to provide solution for that problem.  Depending on the problem, that person can be local, meaning that the person lives or has business in the same area and location as the employer.  The person who is best fit to work on the project is identified and employed to provide a solution to the employer by working on the job.
                            </p>
                            <br />
                            <h5>Step 3</h5>
                            <p>
                                Multiple persons, freelancers, agencies or contractors can apply for that job.  Depending how many people are needed by the person who posted the job, that person may hire one or more people.  
                            </p>
                            <br />
                            <h5>Step 4</h5>
                            <p>
                                The employer confirms that they have hired that person or freelancer by creating a project for that person. The employer can set a schedule and timeline for the project as well as deposit monetary compensation to begin the project. For example, to solve the problem, if the cost is a fixed amount, then the employer can deposit that amount.  
                            </p>
                            <br />
                            <h5>Step 5</h5>
                            <p>
                                After completing the project, the employer can then release the fund to that person or freelancer.  In Trabau, the function of a person in a project that has been executed is viewed as the solution for that project.  After the project is executed, the problem is solved. 
                                   <br />
                                <br />
                                The overall process of problem and solution is viewed in this form.  
                            </p>
                            <ul class="ht-list">
                                <li>
                                    <p>Identify a problem</p>
                                </li>
                                <li>
                                    <p>Create a function to solve that problem</p>
                                </li>
                                <li>
                                    <p>Execute the function</p>
                                </li>
                                <li>
                                    <p>Solve the identified problem.</p>
                                </li>
                            </ul>
                        </div>
                        <div class="tab-pane fade" id="cost" role="tabpanel" aria-labelledby="cost-tab">

                            <h5>Cost and Compensation</h5>
                            <p>
                                Once you create a project and assign a function to a person to help you solve a problem, you will have the option to deposit funds as compensation for that person. The funds will be marked as deposit in your account and you will have option to release it to that person any time before or after the project is completed.  You can also make partial payment at any time while the project is going on.  
                            </p>
                            <p>
                                Currently, we take 10% commission from each freelancer or people who receive money for a project.  This fee can change anytime. At any time during the project or after the project is completed, you have the option to provide bonus to a person who worked on your project to help you solve a problem.
                            </p>
                            <h5>Job Posting (Employer)</h5>
                            <p>If you are posting a job to hire someone to work on a project and you plan to pay that person outside Trabau, there is a $10 fee for posting the job.</p>
                            <h5>Transactional Type Project</h5>
                            <p>For transactional type projects such as buying and selling an item, or sending and receiving money, there will be a $5 fee when the transaction is completed.  Refer to the fee list for more information. For transactional projects, you can deposit funds for the project and release it when the job is done or before the job ends.</p>
                            <p>For transactional projects, you don’t post the item you have for sale or you want to buy. For example, if you are buying or renting a house and you find that listing on another website and you are interested, you can create a project here to buy or rent that house. In this case, you perform all your interactions with that individual here as well as the initial money deposit. If that person already has an account here, you can simply look for his/her user name and add it to the project. If that person does not have an account yet, you can create a link of that project and email it to that person. Once the person clicks on the link that person can then register and be added to the project.</p>
                            <h5>Bringing your own employees</h5>
                            <p>If you create a project where you bring your own people to work on that project, your only cost is the cost for creating the project or hosting the project.  For example, you might need to hire a contractor to replace a water heater for you at your house.  You can create a project here for that contractor.  There will be a $10 fee to host and maintain the project.  If you deposit the money here for the contractor, then the contractor can pay a small fee.</p>
                            <h5>Price List</h5>
                            <p>Currently, we only charge 10% commission from each freelancer or people who receive money for a project.  This price can change at any time. </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="green-strips text-center" id="section_signup" runat="server">
        <div class="container">
            <div class="main-sub-heading mb-4">
                <h2>Sign up to view more profiles</h2>
            </div>
            <a href="signup/" class="cta-btn-md btn-color-white">Sign Up</a>
        </div>
    </section>
</asp:Content>

