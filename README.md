###A proof of concept expert assistant for Betfair historical betting data
#####Project for CA675 Cloud Technologies - M.Sc. Computing (Data Analytics), Dublin City University, 2016
######John Garrigan (15210480), Gerard Kerley (14212835), 
######Paulina Letkiewicz (14211735), Conor Stapleton (14212049)

####Introduction
In the last few months both Microsoft and Facebook have released their bot frameworks to the public, and in doing so they are betting on bots becoming the de facto user interface for apps. Recent advances in natural language processing using deep neural networks, mobile computing, mobile broadband and cloud computing mean that we can all have an intelligent assistant in our pockets. Up to recently it has only been the larger technology companies who have had the technology available to make smart assistants available to the public at scale (Siri, Cortana etc), but with the release of bot frameworks we can expect a wave of apps from former mobile web app developers which are driven by natural language.  

This goal of this project is to make use of Microsoft's recently released bot framework and language understanding intelligence service (LUIS) to build an app which will act as a smart assistant on top of a large dataset of historical betting information from Betfair. In prototyping this smart assistant we wish to examine the bot framework, the LUIS language system, and to build an API layer on top of Google BigQuery to prove how a bot can be built across two public clouds using free tier services to allow voice driven access to a large dataset (~.5 billion records).
