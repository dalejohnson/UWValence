# UWValenceTitle: UWD2L.Valence Desire2Learn API helper classes.

# SUMMARY

UW-Madison’s Learn@UW Team has created a class library which provides a simplified method to authenticate and communicate with BrightSpace’s Desire2Learn learning management system. The goal of library is to provide a controlled and easy method for developers to communicate with Desire2Learn.  This allows developers to focus on developing functionality in in their systems without becoming an expert in Desire2Learn.

## Project Overall Structure / Requirements

Our project includes the UWD2L.Valence library and Demo MVC5 application which demonstrates its use.  The Valence library uses the following NuGet packages; D2L.Extensibility.AuthSdk.Restsharp 1.1.1, D2L.Extensiblity.AuthSdk 1.2.0, and RestSharp 105.1.0. It requires 4.5 net framework.

The library requires the following webconfig / appconfig settings.

| Key | Description |
|:----|:----------- |
|valence_appId / valence_appkey  | The library requires an approved key/id for your each LMS instance.  Keys can be requested at [https://keytool.valence.desire2learn.com](https://keytool.valence.desire2learn.com) |
|lms_host |this is the url for your lms.|
|valence_userID / valence_userKey| Once you have acquired a valence key par you need to generate user ID/Key pair.  This is                                        done using D2L’s test tool [https://apitesttool.desire2learnvalence.com/](https://apitesttool.desire2learnvalence.com/)|


### Methods

#### GetCourseOffering

    Retrieves a course offering

    Parameters: orgUnitID (D2LID) – Org unit ID

    Returns: POC class for D2L’s CourseOffering JSON Object 

#### GetUserData

	Retrieve data for one user

	Parameters: userName – Typically the users netID
	
	Returns: POC class for D2L’s UserData JSON Object 

#### GetUserEnrollments

	Retrieve enrollment details for a user in the provided org.

  Parameters: userName – Typically the users netID

  Returns: POC List of D2L’s UserOrgUnit http://docs.valence.desire2learn.com/res/enroll.html#Enrollment.EnrollmentData

#### EnrollUser

	Enroll existing user in orgUnit.

	Parameters: orgUnitID (D2LID) – Org unit ID
				UserID – D2L User ID (Use GetUserData to lookup)
			   RoleID – D2L ID of the role you wish to enroll them in.

	Returns: Nothing

#### DeleteUser

    Remove enrollment from course.

    Parameters: orgUnitID (D2LID) – Org unit ID
			      UserID – D2L User ID (Use GetUserData to lookup)
		
	Returns: Nothing
